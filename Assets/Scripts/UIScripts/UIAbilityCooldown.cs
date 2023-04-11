using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilityCooldown : MonoBehaviour
{
    [SerializeField] private Image cooldownImage;
    [SerializeField] private Text cooldownTime;
    private Coroutine updateCooldownImageCoroutine;
    private ICooldownable cooldownable;

    public void Init(ICooldownable cooldownable)
    {
        this.cooldownable = cooldownable;
        cooldownable.CooldownStarted += StartCooldown;
        cooldownable.CooldownEnded += EndCooldown;
        Debug.Log("InitCooldownPanel");
    }
    private void StartCooldown()
    {
        Debug.Log("StartCooldown");
        Debug.Log($"ActivePanel= {gameObject.activeSelf}");
        gameObject.SetActive(true);
        Debug.Log($"ActivePanel= {gameObject.activeSelf}");
        if (updateCooldownImageCoroutine != null)
        {
            StopCoroutine(updateCooldownImageCoroutine);
        }
        cooldownImage.fillAmount = 1;
        cooldownTime.text = System.Math.Round(cooldownable.CurrentRemainingTimeCooldown, 1).ToString();
        updateCooldownImageCoroutine = StartCoroutine(UpdateCooldownImage());
    }
    private IEnumerator UpdateCooldownImage()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            cooldownTime.text = System.Math.Round(cooldownable.CurrentRemainingTimeCooldown, 1).ToString();
            cooldownImage.fillAmount = cooldownable.CurrentRemainingTimeCooldown / cooldownable.CurrentCooldown;
        }
    }
    private void EndCooldown()
    {
        gameObject.SetActive(false);
        cooldownImage.fillAmount = 0;
        StopCoroutine(updateCooldownImageCoroutine);
    }
}

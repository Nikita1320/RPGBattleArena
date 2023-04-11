using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private Image coolDownImage;
    [SerializeField] private Button attackButton;
    private float remainingCoolDownTime;
    public Button Button => attackButton;
    public void StartCoolDown(float coolDownTime)
    {
        StartCoroutine(CoolDownCororutine(coolDownTime));
    }
    public void SetActiveButton(bool activeMode)
    {

    }
    private IEnumerator CoolDownCororutine(float coolDownTime)
    {
        coolDownImage.fillAmount = 0;

        float pastTense = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            pastTense += Time.deltaTime;
            coolDownImage.fillAmount = pastTense / coolDownTime;
            if (pastTense / coolDownTime >= 1)
            {
                break;
            }
        }
    }
}

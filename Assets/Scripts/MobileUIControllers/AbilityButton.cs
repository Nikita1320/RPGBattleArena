using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ActiveAbility ability;
    [SerializeField] private Image abilityImage;
    [SerializeField] private UIAbilityCooldown abilityCooldown;
    [SerializeField] private Image blockImage;
    public Button Button => button;
    public void Init(ActiveAbility activeAbility)
    {
        ability = activeAbility;
        abilityImage.sprite = ability.Icon;
        if (ability is ICooldownable cooldownable)
        {
            Debug.Log("IsCooldownable");
            abilityCooldown.Init(cooldownable);
        }
        if (ability.Level == 0)
        {
            LockAbility();
        }
        else
        {
            UnLockAbility();
        }
    }
    public void LockAbility()
    {
        blockImage.gameObject.SetActive(true);
    }
    public void UnLockAbility()
    {
        blockImage.gameObject.SetActive(false);
    }
}

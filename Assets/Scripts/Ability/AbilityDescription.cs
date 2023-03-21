using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDescription : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private Text abilityName;
    [SerializeField] private Text levelText;
    [SerializeField] private Text abilityDescription;
    [SerializeField] private Text nextLevelAbilityDescription;
    [SerializeField] private bool renderNextLevel;
    private Ability ability;
    public Ability Ability => ability;
    public void Init(Ability _ability, int level)
    {
        ability = _ability;
        abilityImage.sprite = ability.Icon;
        levelText.text = "Level: " + level.ToString();
        abilityName.text = ability.NameAbility;
        abilityDescription.text = ability.GetDescription(level);
        if (renderNextLevel)
        {
            string nextLevelDescription = ability.GetDescription(level + 1);
            if (nextLevelDescription != "")
            {
                nextLevelAbilityDescription.text = "On Next Level: \n" + nextLevelDescription;
            }
            else
            {
                nextLevelAbilityDescription.text = "";
            }
        }
    }
}

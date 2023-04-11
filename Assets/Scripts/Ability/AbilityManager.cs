using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private ActiveAbility[] activeAbilities = new ActiveAbility[3];
    [SerializeField] private PassiveAbility[] passiveAbilities = new PassiveAbility[6];

    public ActiveAbility[] ActiveAbilities => activeAbilities;
    public PassiveAbility[] PassiveAbilities => passiveAbilities;
    public void SetImprovement(Character character)
    {
        for (int i = 0; i < activeAbilities.Length; i++)
        {
            activeAbilities[i].Init(character.AbilityTree.AbilityBranches[i].ImprovmentAbility[character.AbilityTree.AbilityBranches[i].ActiveAbility], gameObject);
            Debug.Log(character.AbilityTree.AbilityBranches[i].ImprovmentAbility[character.AbilityTree.AbilityBranches[i].ActiveAbility]);
        }

        passiveAbilities[0].Init(character.AbilityTree.AbilityBranches[0].ImprovmentAbility[character.AbilityTree.AbilityBranches[0].PassiveAbilitys[0]], gameObject);
        passiveAbilities[1].Init(character.AbilityTree.AbilityBranches[0].ImprovmentAbility[character.AbilityTree.AbilityBranches[0].PassiveAbilitys[1]], gameObject);
        passiveAbilities[2].Init(character.AbilityTree.AbilityBranches[1].ImprovmentAbility[character.AbilityTree.AbilityBranches[1].PassiveAbilitys[0]], gameObject);
        passiveAbilities[3].Init(character.AbilityTree.AbilityBranches[1].ImprovmentAbility[character.AbilityTree.AbilityBranches[1].PassiveAbilitys[1]], gameObject);
        passiveAbilities[4].Init(character.AbilityTree.AbilityBranches[2].ImprovmentAbility[character.AbilityTree.AbilityBranches[2].PassiveAbilitys[0]], gameObject);
        passiveAbilities[5].Init(character.AbilityTree.AbilityBranches[2].ImprovmentAbility[character.AbilityTree.AbilityBranches[2].PassiveAbilitys[1]], gameObject);
    }

    public void UseAbility(int index)
    {
        if (activeAbilities[index].Level > 0)
        {
            activeAbilities[index].Use();
        }
    }
}

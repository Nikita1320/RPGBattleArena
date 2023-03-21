using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Reset();
public delegate void FirstImproveAbility(Ability ability);
public class AbilityTree
{
    [SerializeField] private AbilityBranch[] abilityBranches = new AbilityBranch[3];
    [SerializeField] private AbilityManager abilityManager;
    public FirstImproveAbility firstImproveAbilityEvent;
    public Reset resetEvent;
    private Character character;
    private int baseAmmountPoints = 5;
    private int usedPoints;

    public int TotalPoints => baseAmmountPoints + character.Rank;
    public int FreePoints => baseAmmountPoints + character.Rank - usedPoints;
    public AbilityBranch[] AbilityBranches => abilityBranches;
    public AbilityTree(Character character)
    {
        this.character = character;

        this.abilityManager = character.CharacterData.PrefabCharacter.GetComponent<AbilityManager>();

        Debug.Log(character.CharacterData.PrefabCharacter);
        Debug.Log(abilityManager);
        abilityBranches[0] = new(this.abilityManager.ActiveAbilities[0], this.abilityManager.PassiveAbilities[0], this.abilityManager.PassiveAbilities[1]);
        abilityBranches[1] = new(this.abilityManager.ActiveAbilities[1], this.abilityManager.PassiveAbilities[2], this.abilityManager.PassiveAbilities[3]);
        abilityBranches[2] = new(this.abilityManager.ActiveAbilities[2], this.abilityManager.PassiveAbilities[4], this.abilityManager.PassiveAbilities[5]);
    }
    public void UpgradeAbility(int abilityBranch, Ability ability)
    {
        if (abilityBranches[abilityBranch].ImprovmentAbility[ability] == 0)
        {
            firstImproveAbilityEvent?.Invoke(ability);
        }
        abilityBranches[abilityBranch].ImproveAbility(ability);
        usedPoints++;
    }
    public void ResetImprovementAbility()
    {
        foreach (var item in abilityBranches)
        {
            item.ResetImprovement();
        }
    }
}

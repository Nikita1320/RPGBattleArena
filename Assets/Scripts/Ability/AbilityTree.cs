using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Reset();
public delegate void FirstImproveAbility(Ability ability);
public class AbilityTree
{
    [SerializeField] private AbilityBranch[] abilityBranches = new AbilityBranch[3];
    [SerializeField] private AbilityManager abilityManager;
    private int[] openBranchRanks = new int[3] { 0, 3, 5 };
    private int maxLevelActiveAbility = 3;
    private int maxLevelPassiveAbility = 3;
    private int stepImprovedPassiveAbility = 2;
    public FirstImproveAbility firstImproveAbilityEvent;
    public Reset resetEvent;
    private Character character;
    private int baseAmmountPoints = 5;
    private int usedPoints = 0;

    public int MaxLevelActiveAbility => maxLevelActiveAbility;
    public int MaxLevelPassiveAbility => maxLevelPassiveAbility;
    public int StepImprovedPassiveAbility => stepImprovedPassiveAbility;
    public int[] OpenBranchRanks => openBranchRanks;
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
    public AbilityTree(Character character, int[,] improvementAbility)
    {
        this.character = character;

        this.abilityManager = character.CharacterData.PrefabCharacter.GetComponent<AbilityManager>();

        Debug.Log(character.CharacterData.PrefabCharacter);
        Debug.Log(abilityManager);
        abilityBranches[0] = new(this.abilityManager.ActiveAbilities[0], this.abilityManager.PassiveAbilities[0], this.abilityManager.PassiveAbilities[1], new int[3] { improvementAbility[0,0], improvementAbility[0, 1], improvementAbility[0, 2] });
        abilityBranches[1] = new(this.abilityManager.ActiveAbilities[1], this.abilityManager.PassiveAbilities[2], this.abilityManager.PassiveAbilities[3], new int[3] { improvementAbility[1, 0], improvementAbility[1, 1], improvementAbility[1, 2] });
        abilityBranches[2] = new(this.abilityManager.ActiveAbilities[2], this.abilityManager.PassiveAbilities[4], this.abilityManager.PassiveAbilities[5], new int[3] { improvementAbility[2, 0], improvementAbility[2, 1], improvementAbility[2, 2] });
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
    public void RandomImprove()
    {
        var maxPointsMayUse = 0;
        var openBranch = 0;
        if (character.Rank > OpenBranchRanks[2])
        {
            openBranch = 2;
            maxPointsMayUse = (maxLevelActiveAbility * openBranch + maxLevelPassiveAbility * openBranch * 2);
        }
        else if (character.Rank > OpenBranchRanks[1])
        {
            openBranch = 1;
            maxPointsMayUse = (maxLevelActiveAbility * openBranch + maxLevelPassiveAbility * openBranch * 2);
        }
        else if (character.Rank > OpenBranchRanks[0])
        {
            openBranch = 0;
            maxPointsMayUse = (maxLevelActiveAbility * openBranch + maxLevelPassiveAbility * openBranch * 2);
        }
        else
        {
            return;
        }

        var points = FreePoints;
        if (FreePoints > maxPointsMayUse)
        {
            points = maxPointsMayUse;
        }

        while (maxPointsMayUse < 0)
        {
            for (int j = 0; j < openBranch; j++)
            {
                Debug.Log("TickMain");
                if (abilityBranches[j].ImprovmentAbility[abilityBranches[j].ActiveAbility] < maxLevelActiveAbility)
                {
                    abilityBranches[j].ImproveAbility(abilityBranches[j].ActiveAbility);
                    usedPoints++;
                    maxPointsMayUse--;
                    if (FreePoints == 0 || maxPointsMayUse == 0)
                    {
                        return;
                    }
                }
                for (int l = 0; l < 2; l++)
                {
                    Debug.Log("Tick");
                    if (abilityBranches[j].ImprovmentAbility[abilityBranches[j].PassiveAbilitys[l]] < maxLevelPassiveAbility)
                    {
                        abilityBranches[j].ImproveAbility(abilityBranches[j].PassiveAbilitys[l]);
                        usedPoints++;
                        maxPointsMayUse--;
                        if (FreePoints == 0 || maxPointsMayUse == 0)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}

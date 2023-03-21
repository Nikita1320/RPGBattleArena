using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBranch
{
    [SerializeField] private ActiveAbility activeAbility;
    [SerializeField] private PassiveAbility[] passiveAbilitys = new PassiveAbility[2];
    private Dictionary<Ability, int> improvmentAbility = new();
    private Ability[] abilities = new Ability[3];
    public ActiveAbility ActiveAbility => activeAbility;
    public PassiveAbility[] PassiveAbilitys => passiveAbilitys;
    public Ability[] Abilities => abilities;
    public Dictionary<Ability, int> ImprovmentAbility => improvmentAbility;
    public AbilityBranch(ActiveAbility activeAbility, PassiveAbility firstPassiveAbilitys, PassiveAbility secondPassiveAbilitys)
    {
        this.activeAbility = activeAbility;
        passiveAbilitys[0] = firstPassiveAbilitys;
        passiveAbilitys[1] = secondPassiveAbilitys;

        improvmentAbility.Add(activeAbility, 0);
        improvmentAbility.Add(passiveAbilitys[0], 0);
        improvmentAbility.Add(passiveAbilitys[1], 0);

        abilities[0] = activeAbility;
        abilities[1] = firstPassiveAbilitys;
        abilities[2] = secondPassiveAbilitys;
    }
    public AbilityBranch(ActiveAbility activeAbility, PassiveAbility firstPassiveAbilitys, PassiveAbility secondPassiveAbilitys, int[] imrovevmentLevel)
    {
        this.activeAbility = activeAbility;
        passiveAbilitys[0] = firstPassiveAbilitys;
        passiveAbilitys[1] = secondPassiveAbilitys;

        improvmentAbility.Add(activeAbility, imrovevmentLevel[0]);
        improvmentAbility.Add(passiveAbilitys[0], imrovevmentLevel[1]);
        improvmentAbility.Add(passiveAbilitys[1], imrovevmentLevel[2]);

        abilities[0] = activeAbility;
        abilities[1] = firstPassiveAbilitys;
        abilities[2] = secondPassiveAbilitys;

        improvmentAbility[activeAbility] = 0;
    }
    public void ImproveAbility(Ability ability)
    {
        improvmentAbility[ability]++;
    }

    public void ResetImprovement()
    {
        foreach (var item in improvmentAbility.Keys)
        {
            improvmentAbility[item] = 0;
        }
    }
}

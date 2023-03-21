using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private ActiveAbility[] activeAbilities = new ActiveAbility[3];
    [SerializeField] private PassiveAbility[] passiveAbilities = new PassiveAbility[6];

    public ActiveAbility[] ActiveAbilities => activeAbilities;
    public PassiveAbility[] PassiveAbilities => passiveAbilities;
}

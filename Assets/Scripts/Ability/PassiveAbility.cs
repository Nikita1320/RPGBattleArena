using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility : Ability
{
    [SerializeField] private int currentLevel;
    public abstract void Activate();
    public abstract void DisActivate();
}

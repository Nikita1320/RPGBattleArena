using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility : Ability
{
    public override int MaxLevel => 3;
    public abstract void Activate();
}

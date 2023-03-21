using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : Ability
{
    public override int MaxLevel => 5;
    public abstract void Use();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : Ability
{
    [SerializeField] protected bool mayUse = true;
    public bool MayUse => mayUse;
    public abstract bool Use();
}

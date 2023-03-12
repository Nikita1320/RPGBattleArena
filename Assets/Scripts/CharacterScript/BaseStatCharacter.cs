using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BaseStatCharacter
{
    [SerializeField] private float baseValue;
    [SerializeField] private float incrementValue;
    [SerializeField] private TypeStat typeStat;

    public BaseStatCharacter(TypeStat _typeStat)
    {
        typeStat = _typeStat;
    }
    public float BaseValue => baseValue;
    public float IncrementValue => incrementValue;
    public TypeStat TypeStat => typeStat;
}

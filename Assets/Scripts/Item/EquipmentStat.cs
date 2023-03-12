using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeValueStat
{
    Value,
    Percent
}
[System.Serializable]
public class EquipmentStat
{
    [SerializeField] private TypeValueStat typeValue;
    [SerializeField] private TypeStat typeStat;
    [SerializeField] private float[] value;

    public TypeValueStat TypeValue => typeValue;
    public TypeStat TypeStat => typeStat;
    public float GetValue(int level)
    {
        return value[level];
    }
}

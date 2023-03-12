using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterStat
{
    public delegate void ChangedValue();
    public ChangedValue changedValue;

    private Character character;
    private BaseStatCharacter baseStatCharacter;

    [SerializeField] private float valueUpgrade;
    [SerializeField] private float coefficientUpgrade = 1;
    public CharacterStat(Character _character, BaseStatCharacter _baseStatCharacter)
    {
        character = _character;
        baseStatCharacter = _baseStatCharacter;
        character.raisedLevel += () => changedValue?.Invoke();
    }
    public float BaseValue => baseStatCharacter.BaseValue;
    public float IncreseValueByLevel => baseStatCharacter.IncrementValue;

    public float ValueUpgrade { get { return valueUpgrade; } set { valueUpgrade += value; changedValue?.Invoke(); } }
    public float CoefficientUpgrade { get { return coefficientUpgrade; } set { coefficientUpgrade *= value; changedValue?.Invoke(); } }
    public float Value => MathF.Round((BaseValue + (IncreseValueByLevel * character.Level) + valueUpgrade) * coefficientUpgrade,2);
}

[Serializable]
public class FloatStat
{
    public delegate void ChangedValue();
    public ChangedValue changedValue;
    [SerializeField] protected float baseValue;
    [SerializeField] private float increseValueByLevel;
    [SerializeField] private float valueUpgrade;
    [SerializeField] private float coefficientUpgrade;
    public float ValueUpgrade { get { return valueUpgrade; } set { valueUpgrade += value; changedValue?.Invoke(); } }
    public float CoefficientUpgrade { get { return coefficientUpgrade; } set { coefficientUpgrade *= value; changedValue?.Invoke(); } }

    public float Value => (baseValue + valueUpgrade) * coefficientUpgrade;
}
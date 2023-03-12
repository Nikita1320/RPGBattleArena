using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeStatData", menuName = "NewStatData/NewMeleeStat")]
public class MeleeCharacterStatData : CharacterStatData
{
    [SerializeField] private BaseStatCharacter rangeAreaAttack = new(TypeStat.RangeArea);
    [SerializeField] private BaseStatCharacter rangeAttack = new(TypeStat.RangeAttack);

    protected override void Awake()
    {
        if (baseStats.Count <= 0)
        {
            base.Awake();
            baseStats.Add(rangeAreaAttack);
            baseStats.Add(rangeAttack);
        }
    }
    public BaseStatCharacter RangeAreaAttack => rangeAreaAttack;
    public BaseStatCharacter RangeAttack => rangeAttack;
}

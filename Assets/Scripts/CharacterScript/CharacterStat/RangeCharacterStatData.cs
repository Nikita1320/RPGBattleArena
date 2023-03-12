using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRangeStatData", menuName = "NewStatData/NewRangeStat")]
public class RangeCharacterStatData : CharacterStatData
{
    [SerializeField] private BaseStatCharacter lifeTimeBullet = new(TypeStat.LifeTimeBullet);
    [SerializeField] private BaseStatCharacter speedBullet = new(TypeStat.SpeedBullet);

    protected override void Awake()
    {
        if (baseStats.Count <= 0)
        {
            base.Awake();
            baseStats.Add(lifeTimeBullet);
            baseStats.Add(speedBullet);
        }
    }
    public BaseStatCharacter LifeTimeBullet => lifeTimeBullet;
    public BaseStatCharacter SpeedBullet => speedBullet;
}

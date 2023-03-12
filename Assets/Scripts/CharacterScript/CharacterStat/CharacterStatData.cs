using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatData : ScriptableObject
{
    [SerializeField] private BaseStatCharacter damage = new(TypeStat.Damage);
    [SerializeField] private BaseStatCharacter moveSpeed = new(TypeStat.MoveSpeed);
    [SerializeField] private BaseStatCharacter rotateSpeed = new(TypeStat.RotateSpeed);
    [SerializeField] private BaseStatCharacter attackSpeed = new(TypeStat.AttackSpeed);
    [SerializeField] private BaseStatCharacter health = new(TypeStat.Health);
    [SerializeField] private BaseStatCharacter armor = new(TypeStat.Armor);
    [SerializeField] private BaseStatCharacter resistMagic = new(TypeStat.ResistMagic);
    [SerializeField] protected List<BaseStatCharacter> baseStats = new();

    protected virtual void Awake()
    {
        baseStats.Add(damage);
        baseStats.Add(moveSpeed);
        baseStats.Add(rotateSpeed);
        baseStats.Add(attackSpeed);
        baseStats.Add(health);
        baseStats.Add(armor);
        baseStats.Add(resistMagic);
    }
    public BaseStatCharacter Damage => damage;
    public BaseStatCharacter MoveSpeed => moveSpeed;
    public BaseStatCharacter RotateSpeed => rotateSpeed;
    public BaseStatCharacter AttackSpeed => attackSpeed;
    public BaseStatCharacter Health => health;
    public BaseStatCharacter Armor => armor;
    public BaseStatCharacter ResistMagic => resistMagic;
    public BaseStatCharacter[] GetStats()
    {
        return baseStats.ToArray();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void RaisedRank();
public delegate void RaisedLevel();
public delegate void ReachedFirstRank();
public delegate void TakedXP();
public delegate void TakedRankPoint();
public delegate void EquipedItem(TypeEquipment typeEquipment);
public delegate void RemovedItem(TypeEquipment typeEquipment);

public enum TypeStat
{
    Damage,
    MoveSpeed,
    RotateSpeed,
    AttackSpeed,
    Health,
    Armor,
    ResistMagic,
    RangeAttack,
    RangeArea,
    SpeedBullet,
    LifeTimeBullet
}
[Serializable]
public class Character
{
    public RaisedRank raisedRank;
    public RaisedLevel raisedLevel;
    public ReachedFirstRank reachedFirstRank;
    public TakedXP takedXPEvent;
    public TakedRankPoint takedRankPointEvent;
    public EquipedItem equipedItemEvent;
    public RemovedItem removedItemEvent;

    [SerializeField] private CharacterData characterData;

    [SerializeField] private int levelCharacter;
    [SerializeField] private int currentAmountXP;
    [SerializeField] private int rankCharacter;
    [SerializeField] private int currentAmountRankPoint;
    [SerializeField] private bool isOpen;
    [SerializeField] private AbilityTree abilityTree;
    private Dictionary<TypeEquipment, Equipment> characterEquipments = new()
    {
        { TypeEquipment.Hand, null },
        { TypeEquipment.Body, null },
        { TypeEquipment.Head, null },
        { TypeEquipment.Foot, null }
    };
    private Dictionary<TypeStat, CharacterStat> characterStats = new();

    public AbilityTree AbilityTree => abilityTree;
    public Dictionary<TypeStat, CharacterStat> Stats => characterStats;
    public Dictionary<TypeEquipment, Equipment> Equipments => characterEquipments;
    public CharacterData CharacterData => characterData;
    public int Level => levelCharacter;
    public int CurrentAmountXP => currentAmountXP;
    public int Rank => rankCharacter;
    public int CurrentAmountRankPoint => currentAmountRankPoint;
    public bool IsOpen
    {
        get
        {
            if (Rank > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public Character()
    {

    }
    public Character(CharacterData characterData,int rank)
    {
        this.characterData = characterData;
        this.rankCharacter = rank;
        var stats = CharacterData.CharacterStat.GetStats();

        for (int i = 0; i < stats.Length; i++)
        {
            characterStats.Add(stats[i].TypeStat, new CharacterStat(this, stats[i]));
        }
        abilityTree = new(this);
    }
    public Character(CharacterData characterData, int rank, int[,] improvementAbility)
    {
        this.characterData = characterData;
        this.rankCharacter = rank;
        var stats = CharacterData.CharacterStat.GetStats();

        for (int i = 0; i < stats.Length; i++)
        {
            characterStats.Add(stats[i].TypeStat, new CharacterStat(this, stats[i]));
        }
        abilityTree = new(this);
    }
    public void Init()
    {
        var stats = CharacterData.CharacterStat.GetStats();

        for (int i = 0; i < stats.Length; i++)
        {
            characterStats.Add(stats[i].TypeStat, new CharacterStat(this, stats[i]));
        }
        abilityTree = new(this);
    }
    public void Equip(Equipment equipment)
    {
        equipment.ToClothe(this);
        characterEquipments[equipment.EquipmentData.EquipmentType] = equipment;
        equipedItemEvent?.Invoke(equipment.EquipmentData.EquipmentType);
    }
    public void RemoveEquipment(Equipment equipment)
    {
        if (characterEquipments.TryGetValue(equipment.EquipmentData.EquipmentType, out Equipment _equipment))
        {
            _equipment.TakeOff();
        }
        characterEquipments[equipment.EquipmentData.EquipmentType] = null;

        removedItemEvent?.Invoke(equipment.EquipmentData.EquipmentType);
    }
    public void TakeXP(int ammount)
    {
        currentAmountXP += ammount;
        while (true)
        {
            if (currentAmountXP >= CharacterData.CharacterImprovementData.GetPurposePointLevel(Level, CharacterData.Rarity))
            {
                currentAmountXP -= CharacterData.CharacterImprovementData.GetPurposePointLevel(Level, CharacterData.Rarity);
                levelCharacter++;
                raisedLevel?.Invoke();
            }
            else
            {
                break;
            }
        }
        takedXPEvent?.Invoke();
    }
    public void TakeRankPoint(int ammount)
    {
        currentAmountRankPoint += ammount;
        while (true)
        {
            if (currentAmountRankPoint >= CharacterData.CharacterImprovementData.GetPurposePointRank(Rank, CharacterData.Rarity))
            {
                currentAmountRankPoint -= CharacterData.CharacterImprovementData.GetPurposePointRank(Rank,CharacterData.Rarity);
                rankCharacter++;
                if (rankCharacter == 1)
                {
                    reachedFirstRank?.Invoke();
                }
                raisedRank?.Invoke();
            }
            else
            {
                break;
            }
        }
        takedRankPointEvent?.Invoke();
    }
}

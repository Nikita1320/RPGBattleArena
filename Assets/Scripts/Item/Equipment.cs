using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ImprovedStats();
public delegate void ImprovedPerk();
public delegate void ChangedOwner();
[System.Serializable]
public class Equipment
{
    private Character character;
    [SerializeField] private EquipmentData equipmentData;
    [SerializeField] private ItemPerkData itemPerk;
    [SerializeField] private int statsUpgradeLevel = 0;
    [SerializeField] private int perkUpgradeLevel = 0;
    private const int maxLevelStatsUpgrade = 4;
    private const int maxLevelPerkUpgrade = 4;

    public ChangedOwner changedOwnerEvent;
    public ImprovedStats improvedStatsEvent;
    public ImprovedPerk improvedPerkEvent;
    public Removing removingEvent;

    public int MaxLevelStatsUpgrade => maxLevelStatsUpgrade;
    public int MaxLevelPerkUpgrade => maxLevelPerkUpgrade;
    public Character Owner => character;
    public EquipmentData EquipmentData => equipmentData;
    public int CurrentLevelStats => statsUpgradeLevel;
    public int CurrentLevelPerk => perkUpgradeLevel;
    public ItemPerkData EquipmentPerk => itemPerk;

    public Equipment(EquipmentData _equipmentData)
    {
        equipmentData = _equipmentData;
        itemPerk = equipmentData.PossiblePerks[Random.Range(0, equipmentData.PossiblePerks.Length)];
    }
    public void ToClothe(Character _character)
    {
        if (character != null)
        {
            character.RemoveEquipment(this);
        }
        character = _character;

        for (int i = 0; i < equipmentData.EquipmentStat.Length; i++)
        {
            if (equipmentData.EquipmentStat[i].TypeValue == TypeValueStat.Value)
            {
                if(character.Stats.TryGetValue(equipmentData.EquipmentStat[i].TypeStat, out CharacterStat characterStat))
                    characterStat.ValueUpgrade = equipmentData.EquipmentStat[i].GetValue(statsUpgradeLevel);
            }
            else
            {
                if (character.Stats.TryGetValue(equipmentData.EquipmentStat[i].TypeStat, out CharacterStat characterStat))
                    characterStat.CoefficientUpgrade = 1 + equipmentData.EquipmentStat[i].GetValue(statsUpgradeLevel) / 100;
            }
        }
        changedOwnerEvent?.Invoke();
    }
    public void TakeOff()
    {
        for (int i = 0; i < equipmentData.EquipmentStat.Length; i++)
        {
            if (equipmentData.EquipmentStat[i].TypeValue == TypeValueStat.Value)
            {
                if (character.Stats.TryGetValue(equipmentData.EquipmentStat[i].TypeStat, out CharacterStat characterStat))
                    characterStat.ValueUpgrade = -equipmentData.EquipmentStat[i].GetValue(statsUpgradeLevel);
            }
            else
            {
                if (character.Stats.TryGetValue(equipmentData.EquipmentStat[i].TypeStat, out CharacterStat characterStat))
                    characterStat.CoefficientUpgrade = 1 / (1 + equipmentData.EquipmentStat[i].GetValue(statsUpgradeLevel) / 100);
            }
        }

        character = null;
        changedOwnerEvent?.Invoke();
    }
    public void UpgradeStats()
    {
        if (statsUpgradeLevel <= maxLevelStatsUpgrade)
        {
            statsUpgradeLevel++;
            if (character != null)
            {
                for (int i = 0; i < equipmentData.EquipmentStat.Length; i++)
                {
                    if (equipmentData.EquipmentStat[i].TypeValue == TypeValueStat.Value)
                    {
                        if (character.Stats.TryGetValue(equipmentData.EquipmentStat[i].TypeStat, out CharacterStat characterStat))
                            characterStat.ValueUpgrade =
                            (equipmentData.EquipmentStat[i].GetValue(statsUpgradeLevel) - equipmentData.EquipmentStat[i].GetValue(statsUpgradeLevel - 1));
                    }
                    else
                    {
                        if (character.Stats.TryGetValue(equipmentData.EquipmentStat[i].TypeStat, out CharacterStat characterStat))
                        {
                            characterStat.CoefficientUpgrade = 1 / (1 + equipmentData.EquipmentStat[i].GetValue(statsUpgradeLevel - 1) / 100);
                            characterStat.CoefficientUpgrade = 1 + equipmentData.EquipmentStat[i].GetValue(statsUpgradeLevel) / 100;
                        }
                    }
                }
            }
            improvedStatsEvent?.Invoke();
        }
    }
    public void UpgradePerk()
    {
        if (perkUpgradeLevel <= statsUpgradeLevel)
        {
            perkUpgradeLevel++;
            improvedPerkEvent?.Invoke();
        }
    }
    public void InitializeWithParametrs()
    {
        itemPerk = equipmentData.PossiblePerks[Random.Range(0, equipmentData.PossiblePerks.Length)];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RareEquipment
{
    Common,
    Epic,
    Legendary
}
public enum TypeEquipment
{
    Hand,
    Body,
    Head,
    Foot
}
[CreateAssetMenu(fileName = "NewEquipment", menuName = "NewItem/NewEquipment")]
public class EquipmentData : ItemData
{
    [SerializeField] private TypeEquipment typeEquipment;
    [SerializeField] private RareEquipment rareEquipment;
    [SerializeField] private EquipmentStat[] equipmentStat;
    [SerializeField] private ItemPerkData[] possiblePerks;
    public EquipmentStat[] EquipmentStat => equipmentStat;
    public TypeEquipment EquipmentType => typeEquipment;
    public RareEquipment Rare => rareEquipment;
    public ItemPerkData[] PossiblePerks => possiblePerks;
}

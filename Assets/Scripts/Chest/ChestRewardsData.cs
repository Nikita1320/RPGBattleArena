using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rewards Chest", fileName = "NewRewardsChest")]
public class ChestRewardsData : ScriptableObject
{
    [SerializeField] private ChestReward<EquipmentData> commonEquipmentRewards;
    [SerializeField] private ChestReward<EquipmentData> epicEquipmentRewards;
    [SerializeField] private ChestReward<EquipmentData> legendaryEquipmentRewards;
    [SerializeField] private ChestReward<EquipmentData>[] equipmentRewardsWithCustomProbability;

    [SerializeField] private ChestReward<CharacterCardData> commonCharacterCardRewards;
    [SerializeField] private ChestReward<CharacterCardData> epicCharacterCardRewards;
    [SerializeField] private ChestReward<CharacterCardData> legendaryCharacterCardRewards;
    [SerializeField] private ChestReward<CharacterCardData>[] characterCardRewardsWithCustomProbability;

    public ChestReward<EquipmentData> CommonEquipmentRewards => commonEquipmentRewards;
    public ChestReward<EquipmentData> EpicEquipmentRewards => epicEquipmentRewards;
    public ChestReward<EquipmentData> LegendaryEquipmentRewards => legendaryEquipmentRewards;
    public ChestReward<EquipmentData>[] EquipmentRewardsWithCustomProbability => equipmentRewardsWithCustomProbability;
    public ChestReward<CharacterCardData> CommonCharacterCardRewards => commonCharacterCardRewards;
    public ChestReward<CharacterCardData> EpicCharacterCardRewards => epicCharacterCardRewards;
    public ChestReward<CharacterCardData> LegendaryCharacterCardRewards => legendaryCharacterCardRewards;
    public ChestReward<CharacterCardData>[] CharacterCardRewardsWithCustomProbability => characterCardRewardsWithCustomProbability;
}

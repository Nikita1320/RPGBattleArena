using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BattleArenaData", fileName = "newBattleArenaData")]
public class BattleArenaData : ScriptableObject
{
    [SerializeField] private GameObject map;
    [SerializeField] private int numberEnemy;
    [Header("SettingsPossibleCharacters")]
    [SerializeField] private CharacterData[] possibleEnemys;
    [SerializeField] private int minRank;
    [SerializeField] private int maxRank;

    [Header("SettingsPossibleEquipmentsCharacter")]
    [SerializeField] private EquipmentData[] possibleEquipments;
    [SerializeField] private int minLevelUpgradeStatsEquipmment;
    [SerializeField] private int maxLevelUpgradeStatsEquipmment;
    [SerializeField] private int minLevelImprovePerkEquipmment;
    [SerializeField] private int maxLevelImprovePerkEquipmment;

    [SerializeField] private BattleReward[] battleRewardsForPassing;
    [SerializeField] private BattleReward[] battleRewardsForSecondaryPassing;

    public GameObject Map => map;
    public int NumberOfEnemy => numberEnemy;
    public CharacterData[] PossibleEnemys => possibleEnemys;
    public int MinRank => minRank;
    public int MaxRank => maxRank;

    public EquipmentData[] PossibleEquipments => possibleEquipments;
    public int MinLevelUpgradeStatsEquipmment => minLevelUpgradeStatsEquipmment;
    public int MaxLevelUpgradeStatsEquipmment => maxLevelUpgradeStatsEquipmment;
    public int MinLevelImprovePerkEquipmment => minLevelImprovePerkEquipmment;
    public int MaxLevelImprovePerkEquipmment => maxLevelImprovePerkEquipmment;
    public BattleReward[] BattleRewardsForPassing => battleRewardsForPassing;
    public BattleReward[] BattleRewardsForSecondaryPassing => battleRewardsForSecondaryPassing;
}

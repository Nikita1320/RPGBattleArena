using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterImprovementData", fileName = "new CharacterImprovementData")]
public class CharacterImprovementData : ScriptableObject
{
    [SerializeField] private int[] xpLevel;
    [SerializeField] private int maxLevel;

    [SerializeField] private int[] pointRankCommon;
    [SerializeField] private int[] pointRankEpic;
    [SerializeField] private int[] pointRankLegendary;
    [SerializeField] private int maxRank;

    [SerializeField] private Color[] colorWithRarity;

    public int GetPurposePointLevel(int currentLevel, RarityCharacter rarity)
    {
        if (currentLevel == maxLevel)
        {
            return 0;
        }
        else
        {
            return xpLevel[currentLevel];
        }
    }
    public int GetPurposePointRank(int currentRank, RarityCharacter rarity)
    {
        if (rarity == RarityCharacter.Common)
        {
            return pointRankCommon[currentRank];
        }
        if (rarity == RarityCharacter.Epic)
        {
            return pointRankEpic[currentRank];
        }
        if (rarity == RarityCharacter.Legendary)
        {
            return pointRankLegendary[currentRank];
        }
        return 0;
    }
    public Color GetColor(RarityCharacter rarity)
    {
        return colorWithRarity[(int)rarity];
    }
}

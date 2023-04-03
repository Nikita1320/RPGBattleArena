using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private ChestData chestData;
    [SerializeField] private int ammountEquipmentRewards;
    [SerializeField] private int ammountCharacterCardRewards;
    [SerializeField] private List<ChestReward<EquipmentData>> equipmentRewards = new();
    [SerializeField] private List<ChestReward<CharacterCardData>> characterCardRewards = new();

    private float sumEquipmentProbability;
    private float sumCharacterCardProbability;
    public ChestData ChestData => chestData;
    private void Start()
    {
        InitializeRewards();
        foreach (var item in equipmentRewards)
        {
            sumEquipmentProbability += item.Probability;
        }
        foreach (var item in characterCardRewards)
        {
            sumCharacterCardProbability += item.Probability;
        }
    }
    public virtual void GetRewards(int ammountChestOpen, out List<EquipmentData> equipmentRewards, out List<CharacterCardData> characterCardRewards)
    {
        equipmentRewards = GetEquipmentRewards(ammountChestOpen);
        characterCardRewards = GetCharacterCardRewards(ammountChestOpen);
    }
    protected List<EquipmentData> GetEquipmentRewards(int ammountChest)
    {
        var randomValue = Random.value * sumEquipmentProbability;
        Debug.Log("Random value = " + randomValue);
        List<EquipmentData> rewards = new();

        while (rewards.Count < ammountEquipmentRewards * ammountChest)
        {
            foreach (var item in equipmentRewards)
            {
                if (randomValue < item.Probability)
                {
                    Debug.Log("Take");
                    rewards.Add(item.ProbabilityRewards[Random.Range(0, item.ProbabilityRewards.Length)]);
                    break;
                }
                else
                {
                    Debug.Log("Miss");
                    randomValue -= item.Probability;
                }
            }
        }
        return rewards;
    }
    protected List<CharacterCardData> GetCharacterCardRewards(int ammountChest)
    {
        var randomValue = Random.value * sumCharacterCardProbability;
        List<CharacterCardData> rewards = new();

        while (rewards.Count < ammountCharacterCardRewards * ammountChest)
        {
            foreach (var item in characterCardRewards)
            {
                if (randomValue < item.Probability)
                {
                    rewards.Add(item.ProbabilityRewards[Random.Range(0, item.ProbabilityRewards.Length)]);
                    break;
                }
                else
                {
                    randomValue -= item.Probability;
                }
            }
        }
        return rewards;
    }
    private void InitializeRewards()
    {
        equipmentRewards.Add(chestData.RewardsData.CommonEquipmentRewards);
        equipmentRewards.Add(chestData.RewardsData.EpicEquipmentRewards);
        equipmentRewards.Add(chestData.RewardsData.LegendaryEquipmentRewards);
        equipmentRewards.AddRange(chestData.RewardsData.EquipmentRewardsWithCustomProbability);

        characterCardRewards.Add(chestData.RewardsData.CommonCharacterCardRewards);
        characterCardRewards.Add(chestData.RewardsData.EpicCharacterCardRewards);
        characterCardRewards.Add(chestData.RewardsData.LegendaryCharacterCardRewards);
        characterCardRewards.AddRange(chestData.RewardsData.CharacterCardRewardsWithCustomProbability);
    }
}

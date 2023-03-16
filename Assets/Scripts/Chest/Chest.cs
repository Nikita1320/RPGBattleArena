using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private ChestRewardsData rewardsData;
    [SerializeField] private int ammountEquipmentRewards;
    [SerializeField] private int ammountCharacterCardRewards;
    [SerializeField] private List<ChestReward<EquipmentData>> equipmentRewards = new();
    [SerializeField] private List<ChestReward<CharacterCardData>> characterCardRewards = new();

    private float sumEquipmentProbability;
    private float sumCharacterCardProbability;
    public GameObject PrefabChest => chestPrefab;
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
    public virtual void GetRewards(int ammountChestOpen, out List<Equipment> equipmentRewards, out List<CharacterCard> characterCardRewards)
    {
        List<Equipment> equipments = new();
        List<CharacterCard> characterCards = new();

        foreach (var item in GetEquipmentRewards(ammountChestOpen))
        {
            equipments.Add(new Equipment(item));
        }
        equipmentRewards = equipments;

        foreach (var item in GetCharacterCardRewards(ammountChestOpen))
        {
            characterCards.Add(new CharacterCard(item));
        }
        characterCardRewards = characterCards;
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
        equipmentRewards.Add(rewardsData.CommonEquipmentRewards);
        equipmentRewards.Add(rewardsData.EpicEquipmentRewards);
        equipmentRewards.Add(rewardsData.LegendaryEquipmentRewards);
        equipmentRewards.AddRange(rewardsData.EquipmentRewardsWithCustomProbability);

        characterCardRewards.Add(rewardsData.CommonCharacterCardRewards);
        characterCardRewards.Add(rewardsData.EpicCharacterCardRewards);
        characterCardRewards.Add(rewardsData.LegendaryCharacterCardRewards);
        characterCardRewards.AddRange(rewardsData.CharacterCardRewardsWithCustomProbability);
    }
}

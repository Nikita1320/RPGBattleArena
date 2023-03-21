using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chest", fileName = "NewChest")]
public class ChestData : ScriptableObject
{
    [SerializeField] private string nameChest;
    [SerializeField] private string descriptionChest;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private ChestRewardsData rewardsData;
    public string NameChest => nameChest;
    public string DescriptionChest => descriptionChest;
    public GameObject ChestPrefab => chestPrefab;
    public ChestRewardsData RewardsData => rewardsData;
}

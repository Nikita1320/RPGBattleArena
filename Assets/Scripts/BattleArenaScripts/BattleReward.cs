using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleReward
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int minAmmount;
    [SerializeField] private int maxAmmount;

    public ResourceType ResourceType => resourceType;
    public int MinAmmount => minAmmount;
    public int MaxAmmount => maxAmmount;
}

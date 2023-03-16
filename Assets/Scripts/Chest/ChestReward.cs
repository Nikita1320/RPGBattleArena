using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChestReward<T> where T:ItemData
{
    [SerializeField] private T[] probabilityRewards;
    [SerializeField] private float probability;

    public T[] ProbabilityRewards => probabilityRewards;
    public float Probability => probability;
}

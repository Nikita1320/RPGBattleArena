using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardCellWithUniqueProbability<T> : MonoBehaviour
{
    [SerializeField] protected Text probabilityText;
    [SerializeField] protected RewardCell<T> rewardCell;
    public void Init(T reward, float probability)
    {
        rewardCell.Init(reward);
        probabilityText.text = (probability * 100).ToString() + "%";
    }
}

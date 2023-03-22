using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class RewardCell<T> : MonoBehaviour
{
    [SerializeField] protected Image rewardImage;
    [SerializeField] protected Image backGroundImage;
    public abstract void Init(T rewardObject);
}

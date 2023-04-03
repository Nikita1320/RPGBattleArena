using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceRewardCell : RewardCell<ResourceReward>
{
    [SerializeField] private Text ammountText;
    [SerializeField] private Color backGroundColor;
    public override void Init(ResourceReward rewardObject)
    {
        ammountText.text = rewardObject.Ammount.ToString();
        backGroundImage.color = backGroundColor;
        rewardImage.sprite = Bank.Instance.Resources[rewardObject.ResourceType].ResourceSprite;
    }
}

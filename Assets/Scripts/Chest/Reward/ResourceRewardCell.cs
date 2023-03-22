using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceRewardCell : RewardCell<Resource>
{
    [SerializeField] private Text ammountText;
    [SerializeField] private Color backGroundColor;
    public override void Init(Resource rewardObject)
    {
        ammountText.text = rewardObject.Ammount.ToString();
        backGroundImage.color = backGroundColor;
        rewardImage.sprite = rewardObject.ResourceSprite;
    }
}

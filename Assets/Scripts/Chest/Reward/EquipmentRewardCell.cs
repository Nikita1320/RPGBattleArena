using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentRewardCell : RewardCell<EquipmentData>
{
    [SerializeField] private Color[] rareColor;
    public override void Init(EquipmentData rewardObject)
    {
        rewardImage.sprite = rewardObject.SpriteItem;
        backGroundImage.color = rareColor[(int)rewardObject.Rare];
    }
}

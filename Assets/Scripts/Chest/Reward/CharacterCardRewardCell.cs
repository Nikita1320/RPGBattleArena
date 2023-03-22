using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardRewardCell : RewardCell<CharacterCardData>
{
    public override void Init(CharacterCardData rewardObject)
    {
        rewardImage.sprite = rewardObject.SpriteItem;
        backGroundImage.color = rewardObject.ForCharacter.RarityColor;
    }
}

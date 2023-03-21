using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardRewardCell : RewardCell<CharacterCardData>
{
    [SerializeField] private Image rareImage;
    public override void Init(CharacterCardData rewardObject)
    {
        rewardImage.sprite = rewardObject.SpriteItem;
        rareImage.color = rewardObject.ForCharacter.RarityColor;
    }
}

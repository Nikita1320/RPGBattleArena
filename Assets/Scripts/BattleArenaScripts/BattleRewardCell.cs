using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRewardCell : MonoBehaviour
{
    [SerializeField] private Image resorceIcon;
    [SerializeField] private Text ammountReward;
    [SerializeField] private BattleReward battleReward;

    public void Init(BattleReward battleReward)
    {
        this.battleReward = battleReward;
        resorceIcon.sprite = Bank.Instance.Resources[battleReward.ResourceType].ResourceSprite;
        ammountReward.text = $"{battleReward.MinAmmount} - {battleReward.MaxAmmount}";
    }
}

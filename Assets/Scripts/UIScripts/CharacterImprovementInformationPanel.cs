using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterImprovementInformationPanel : MonoBehaviour
{
    private Character character;
    [SerializeField] private Text currentLevel;
    [SerializeField] private Text currentRank;
    [SerializeField] private Text currentAmmountXP;
    [SerializeField] private Text currentAmmountRankPoint;
    [SerializeField] private Text purposeAmmountXP;
    [SerializeField] private Text purposeAmmountRankPoint;
    [SerializeField] private Image progressRank;
    [SerializeField] private Image progressLevel;
    public void Init(Character _character)
    {
        if (character != null)
        {
            character.raisedLevel -= UpdateLevel;
            character.raisedRank -= UpdateRank;
            character.takedXPEvent -= UpdAteCurrentAmmountXP;
            character.takedRankPointEvent -= UpdateCurrentAmmountRankPoint;
        }

        character = _character;
        UpdateLevel();
        UpdateRank();
        UpdAteCurrentAmmountXP();
        UpdateCurrentAmmountRankPoint();

        character.raisedLevel += UpdateLevel;
        character.raisedRank += UpdateRank;
        character.takedXPEvent += UpdAteCurrentAmmountXP;
        character.takedRankPointEvent += UpdateCurrentAmmountRankPoint;
    }
    private void UpdateLevel()
    {
        currentLevel.text = character.Level.ToString();
        purposeAmmountXP.text = character.CharacterData.CharacterImprovementData.GetPurposePointLevel(character.Level, character.CharacterData.Rarity).ToString();
    }
    private void UpdateRank()
    {
        currentRank.text = character.Rank.ToString();
        purposeAmmountRankPoint.text = character.CharacterData.CharacterImprovementData.GetPurposePointRank(character.Rank, character.CharacterData.Rarity).ToString();
    }
    private void UpdAteCurrentAmmountXP()
    {
        currentAmmountXP.text = character.CurrentAmountXP.ToString();
        progressLevel.fillAmount = (float)character.CurrentAmountXP / (float)character.CharacterData.CharacterImprovementData.GetPurposePointLevel(character.Level, character.CharacterData.Rarity);
    }
    private void UpdateCurrentAmmountRankPoint()
    {
        currentAmmountRankPoint.text = character.CurrentAmountRankPoint.ToString();
        progressRank.fillAmount = (float)character.CurrentAmountRankPoint / (float)character.CharacterData.CharacterImprovementData.GetPurposePointRank(character.Rank, character.CharacterData.Rarity);
    }
    public void ResetPanel()
    {
        character.raisedLevel -= UpdateLevel;
        character.raisedRank -= UpdateRank;
        character.takedXPEvent -= UpdAteCurrentAmmountXP;
        character.takedRankPointEvent -= UpdateCurrentAmmountRankPoint;
        character = null;
    }
}

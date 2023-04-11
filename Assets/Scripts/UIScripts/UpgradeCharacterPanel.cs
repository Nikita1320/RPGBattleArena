using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCharacterPanel : MonoBehaviour
{
    private Character character;
    [SerializeField] private Inventory inventory;
    [SerializeField] private CharacterCardData[] cards;
    [SerializeField] private CharacterCardCell characterCardCell;
    [SerializeField] private Text currentRank;
    [SerializeField] private Image upgradeProgressImage;
    [SerializeField] private Image upgradeImage;
    [SerializeField] private Text currentAmmountRankPoint;
    [SerializeField] private Text purposeAmmountRankPoint;
    [SerializeField] private Image progressRank;

    [SerializeField] private Button upAmmountButton;
    [SerializeField] private Button downAmmountButton;
    [SerializeField] private Button applyUpgradeButton;
    [SerializeField] private int currentAmmountCardSelected = 0;
    private CharacterCard neededCardToUpgradeCharacterRank;

    private void Awake()
    {
        inventory = Inventory.Instance;
    }
    public void Init(Character _character)
    {
        if (inventory == null)
        {
            inventory = Inventory.Instance;
        }
        if (character != null)
        {
            character.raisedRank -= UpdateRank;
            character.takedRankPointEvent -= UpdateCurrentAmmountRankPoint;
        }

        neededCardToUpgradeCharacterRank = null;
        character = _character;

        var neededCard = DeterminingNeededCard();
        neededCardToUpgradeCharacterRank = FindNeededCard(neededCard);

        if (neededCardToUpgradeCharacterRank == null)
        {
            SetEmptySlot(neededCard);
        }
        else
        {
            SetCharacterCardToCell();
        }

        UpdateRank();
        UpdateCurrentAmmountRankPoint();

        character.raisedRank += UpdateRank;
        character.takedRankPointEvent += UpdateCurrentAmmountRankPoint;
    }
    private void UpdateRank()
    {
        currentRank.text = character.Rank.ToString();
        purposeAmmountRankPoint.text = character.CharacterData.CharacterImprovementData.GetPurposePointRank(character.Rank, character.CharacterData.Rarity).ToString();
    }
    private void UpdateCurrentAmmountRankPoint()
    {
        currentAmmountRankPoint.text = character.CurrentAmountRankPoint.ToString();
        progressRank.fillAmount = (float)character.CurrentAmountRankPoint / (float)character.CharacterData.CharacterImprovementData.GetPurposePointRank(character.Rank, character.CharacterData.Rarity);
    }
    public CharacterCardData DeterminingNeededCard()
    {
        foreach (var item in cards)
        {
            if (item.ForCharacter == character.CharacterData)
            {
                return item;
            }
        }
        return null;
    }
    public CharacterCard FindNeededCard(CharacterCardData neededCard)
    {
        for (int i = 0; i < inventory.CharacterCards.Length; i++)
        {
            if (inventory.CharacterCards[i].CardData == neededCard)
            {
                return inventory.CharacterCards[i];
            }
        }
        return null;
    }
    public void UpAmmount()
    {
        currentAmmountCardSelected++;

        if (currentAmmountCardSelected == neededCardToUpgradeCharacterRank.Ammount)
        {
            upAmmountButton.interactable = false;
        }

        characterCardCell.AmmountText.text = (neededCardToUpgradeCharacterRank.Ammount - currentAmmountCardSelected).ToString();
        downAmmountButton.interactable = true;

        if (character.CurrentAmountRankPoint + currentAmmountCardSelected > character.CharacterData.CharacterImprovementData.GetPurposePointRank(character.Rank, character.CharacterData.Rarity))
        {
            upgradeImage.gameObject.SetActive(true);
        }
        UpdateProgressRankImageWithPossibleUpgrade();
    }
    public void DownAmmount()
    {
        currentAmmountCardSelected--;
        if (currentAmmountCardSelected == 0)
        {
            downAmmountButton.interactable = false;
        }

        characterCardCell.AmmountText.text = (neededCardToUpgradeCharacterRank.Ammount - currentAmmountCardSelected).ToString();

        if ((character.CurrentAmountRankPoint + currentAmmountCardSelected) < character.CharacterData.CharacterImprovementData.GetPurposePointRank(character.Rank, character.CharacterData.Rarity))
        {
            upgradeImage.gameObject.SetActive(false);
        }
        UpdateProgressRankImageWithPossibleUpgrade();
    }
    public void ApplyUpgradeButton()
    {
        if (currentAmmountCardSelected > 0)
        {
            character.TakeRankPoint(currentAmmountCardSelected);
            neededCardToUpgradeCharacterRank.ChangeAmmount(-currentAmmountCardSelected);
            upgradeImage.gameObject.SetActive(false);
            currentAmmountCardSelected = 0;

            UpdateProgressRankImageWithPossibleUpgrade();
            if (neededCardToUpgradeCharacterRank.Ammount == 0)
            {
                inventory.RemoveCardItem(neededCardToUpgradeCharacterRank);
            }
        }
    }
    private void UpdateProgressRankImageWithPossibleUpgrade()
    {
        var temp = (float)(character.CurrentAmountRankPoint + currentAmmountCardSelected) / (float)character.CharacterData.CharacterImprovementData.GetPurposePointRank(character.Rank, character.CharacterData.Rarity);
        Debug.Log($"fillAmmountUpgradeImage = {temp}");
        if (temp > 1)
        {
            upgradeProgressImage.fillAmount = 1;
        }
        else
        {
            upgradeProgressImage.fillAmount = temp;
        }
    }
    private void OnDisable()
    {
        ResetPanel();
    }
    private void SetEmptySlot(CharacterCardData cardData)
    {
        characterCardCell.ItemImage.sprite = cardData.SpriteItem;
        characterCardCell.AmmountText.text = "0";
        characterCardCell.RareImage.color = cardData.ForCharacter.RarityColor;
        upAmmountButton.interactable = false;
        downAmmountButton.interactable = false;
    }
    private void SetCharacterCardToCell()
    {
        characterCardCell.Init(neededCardToUpgradeCharacterRank);
        if (neededCardToUpgradeCharacterRank.Ammount > 0)
        {
            upAmmountButton.interactable = true;
            downAmmountButton.interactable = false;
        }
    }
    public void ResetPanel()
    {
        currentAmmountCardSelected = 0;

        if (neededCardToUpgradeCharacterRank != null)
        {
            upAmmountButton.interactable = true;
            downAmmountButton.interactable = false;

            characterCardCell.AmmountText.text = neededCardToUpgradeCharacterRank.Ammount.ToString();
        }
        upgradeImage.gameObject.SetActive(false);
    }
}

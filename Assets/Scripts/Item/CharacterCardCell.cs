using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardCell : ItemCell
{
    [SerializeField] private Color[] rareCardColor;
    [SerializeField] private Text ammountText;
    private CharacterCard characterCard;

    public Text AmmountText => ammountText;
    public CharacterCard CharacterCard => characterCard;
    public void Init(CharacterCard characterCard)
    {
        if (this.characterCard != null)
        {
            characterCard.changedAmmountEvent -= UpdateAmmount;
        }
        this.characterCard = characterCard;
        itemImage.sprite = characterCard.CardData.SpriteItem;
        rareImage.color = rareCardColor[(int)characterCard.CardData.ForCharacter.Rarity];
        ammountText.text = characterCard.Ammount.ToString();
        characterCard.changedAmmountEvent += UpdateAmmount;
    }
    public void UpdateAmmount()
    {
        ammountText.text = characterCard.Ammount.ToString();
    }
    private void OnDestroy()
    {
        characterCard.changedAmmountEvent -= UpdateAmmount;
    }
}

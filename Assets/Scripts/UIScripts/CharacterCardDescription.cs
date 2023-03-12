using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardDescription : MonoBehaviour
{
    private CharacterCard characterCard;
    [SerializeField] private CharacterCardCell cardCell;
    [SerializeField] private Text nameText;
    [SerializeField] private Text intendedCharacterName;
    [SerializeField] private Image intendedCharacterImage;
    [SerializeField] private GameObject descriptionPanel;

    public void Init(CharacterCard _characterCard)
    {
        if (characterCard != _characterCard)
        {
            descriptionPanel.SetActive(true);

            characterCard = _characterCard;

            cardCell.gameObject.SetActive(true);
            cardCell.Init(characterCard);

            nameText.text = characterCard.CardData.NameItem;

            intendedCharacterName.text = characterCard.CardData.ForCharacter.Name;

            intendedCharacterImage.gameObject.SetActive(true);
            intendedCharacterImage.sprite = characterCard.CardData.ForCharacter.SpriteCharacter;
        }
    }
}

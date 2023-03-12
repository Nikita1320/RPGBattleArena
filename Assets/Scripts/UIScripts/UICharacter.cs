using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour
{
    private static UICharacter selected;
    private static Color selectionColor = Color.yellow;

    [SerializeField] private Character character;
    [SerializeField] private CharactersManager charactersManager;

    [SerializeField] private Image imageCharacter;
    [SerializeField] private Text lvlCharacter;

    [SerializeField] private Image blockImage;
    [SerializeField] private Color blockColor;

    private void Start()
    {
        charactersManager = GetComponentInParent<CharactersManager>();
    }
    public void Select()
    {
        charactersManager.SelectCharacter(character);
        HighlightConteiner(this);
    }
    public void Init(Character _character)
    {
        character = _character;
        imageCharacter.sprite = character.CharacterData.SpriteCharacter;
        lvlCharacter.text = character.Level.ToString();
        if (!_character.IsOpen)
        {
            blockImage.gameObject.SetActive(true);
            imageCharacter.color = blockColor;
        }
        character.reachedFirstRank += UpdateImage;
        character.raisedLevel += UpdateLevel;
    }
    public void UpdateImage()
    {
        blockImage.gameObject.SetActive(false);
        imageCharacter.color = Color.white;

    }
    public void UpdateLevel()
    {
        lvlCharacter.text = character.Level.ToString();
    }
    private static void HighlightConteiner(UICharacter uICharacter)
    {
        if (selected != null)
        {
            RemoveHighlightConteiner();
        }
        selected = uICharacter;
        selected.GetComponent<Image>().color = selectionColor;
    }
    private static void RemoveHighlightConteiner()
    {
        selected.GetComponent<Image>().color = Color.white;
        selected = null;
    }
}

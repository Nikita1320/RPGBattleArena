using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUICell : MonoBehaviour
{
    private Character character;

    [SerializeField] private Image imageCharacter;
    [SerializeField] private Text lvlCharacter;

    [SerializeField] private Image blockImage;
    [SerializeField] private Color blockColor;
    [SerializeField] private Color selectionColor;
    [SerializeField] private Button button;
    public Button CellButton => button;
    public Character Character => character;
    private void Start()
    {
        button = GetComponent<Button>();
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
    public void HighlightConteiner()
    {
        GetComponent<Image>().color = selectionColor;
    }
    public void RemoveHighlightConteiner()
    {
        GetComponent<Image>().color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelCharacter : MonoBehaviour
{
    [SerializeField] private Text lvlText;
    private Character character;
    private CharactersManager charactersManager;
    void Start()
    {
        charactersManager = CharactersManager.Instance;
        charactersManager.changedCharacter += SubscribeOnCharacter;
        character = charactersManager.SelectedCharacter;
        if (character == null)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    private void UpdateLvl()
    {
        lvlText.text = character.Level.ToString();
    }

    private void SubscribeOnCharacter(Character _character)
    {
        gameObject.SetActive(true);
        if (character != _character)
        {
            if (character != null)
            {
                character.raisedLevel -= UpdateLvl;
            }
            character = _character;
            character.raisedLevel += UpdateLvl;
            UpdateLvl();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRankCharacter : MonoBehaviour
{
    [SerializeField] private Text rankText;
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
    private void UpdateRank()
    {
        rankText.text = character.Rank.ToString();
    }

    private void SubscribeOnCharacter(Character _character)
    {
        gameObject.SetActive(true);
        if (character != _character)
        {
            if (character != null)
            {
                character.raisedRank -= UpdateRank;
            }
            character = _character;
            character.raisedRank += UpdateRank;
            UpdateRank();
        }
    }
}

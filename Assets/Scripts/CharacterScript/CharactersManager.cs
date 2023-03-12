using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChangedCharacter(Character character);
public class CharactersManager : MonoBehaviour
{
    public ChangedCharacter changedCharacter;

    private static CharactersManager instance;

    private Character selectedCharacter;

    [SerializeField] private CharacterData[] charactersSO;
    [SerializeField] private Character[] characters;

    public Character SelectedCharacter => selectedCharacter;
    public static CharactersManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].Init();
        }
    }
    public void SelectCharacter(Character character)
    {
        selectedCharacter = character;
        changedCharacter?.Invoke(selectedCharacter);
    }

    public Character[] GetCharactersByRarity(RarityCharacter rarity)
    {
        List<Character> sortedCharacters = new();
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].CharacterData.Rarity == rarity)
            {
                sortedCharacters.Add(characters[i]);
            }
        }
        return SortByAvailability(sortedCharacters).ToArray();
    }

    private List<Character> SortByAvailability(List<Character> _characters)
    {
        List<Character> notAvailableCharacter = new();
        List<Character> availableCharacter = new();
        for (int i = 0; i < _characters.Count; i++)
        {
            if (_characters[i].IsOpen == true)
            {
                availableCharacter.Add(_characters[i]);
            }
            else
            {
                notAvailableCharacter.Add(_characters[i]);
            }
        }
        if (availableCharacter.Count > 1)
        {
            for (int i = 0; i < availableCharacter.Count - 1; i++)
            {
                for (int j = i + 1; j < availableCharacter.Count; j++)
                {
                    if (availableCharacter[i].Level < availableCharacter[j].Level)
                    {
                        var temp = availableCharacter[i];
                        availableCharacter[i] = availableCharacter[j];
                        availableCharacter[j] = temp;
                    }
                }
            }
        }
        var result = availableCharacter;

        result.InsertRange(availableCharacter.Count, notAvailableCharacter);

        return result;
    }
}

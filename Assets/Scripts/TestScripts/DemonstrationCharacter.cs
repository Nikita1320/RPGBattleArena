using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonstrationCharacter : MonoBehaviour
{
    [SerializeField] private Transform instancePoint;
    private Character character;
    private GameObject demonstrationPrefab;
    private CharactersManager charactersManager;
    private void Start()
    {
        charactersManager = CharactersManager.Instance;
        charactersManager.changedCharacter += InstantiateDemonstrationCharacter;
    }

    public void InstantiateDemonstrationCharacter(Character _character)
    {
        if (_character != character)
        {
            if (demonstrationPrefab != null)
            {
                Destroy(demonstrationPrefab);
            }
            character = _character;

            demonstrationPrefab = Instantiate(character.CharacterData.DemonstrationPrefabCharacter);
            demonstrationPrefab.transform.position = instancePoint.position;
            demonstrationPrefab.transform.Rotate(0, 180, 0);
            demonstrationPrefab.transform.SetParent(transform);
        }
    }
}

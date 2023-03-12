using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "NewItem/NewCard")]
public class CharacterCardData: ItemData
{
    [SerializeField] private CharacterData character;
    public CharacterData ForCharacter => character;
}

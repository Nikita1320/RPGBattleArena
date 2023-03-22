using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCell : MonoBehaviour
{
    [SerializeField] Image enemySprite;
    private CharacterData characterData;
    public void Init(CharacterData characterData)
    {
        this.characterData = characterData;
        enemySprite.sprite = characterData.SpriteCharacter;
    }
}

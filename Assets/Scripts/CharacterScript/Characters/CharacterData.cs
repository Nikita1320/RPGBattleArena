using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeAttackCharacter
{
    Range,
    Melee
}
public enum RarityCharacter
{
    Common,
    Epic,
    Legendary
}
[CreateAssetMenu(menuName = "CharacterSO", fileName = "new CharacterSO")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private RarityCharacter rarityCharacter;
    [SerializeField] private TypeAttackCharacter typeAttack;
    [SerializeField] private CharacterImprovementData characterImprovementData;

    [SerializeField] private GameObject prefabCharacter;
    [SerializeField] private GameObject demonstrationPrefabCharacter;

    [SerializeField] private Sprite spriteCharacter;
    [SerializeField] private string nameCharacter;
    [SerializeField] private string descriptionCharacter;

    [SerializeField] private CharacterStatData characterStatData;
    //[SerializeField] private AbilityTree abilityTree;

    public RarityCharacter Rarity => rarityCharacter;
    public CharacterImprovementData CharacterImprovementData => characterImprovementData;
    public GameObject DemonstrationPrefabCharacter => demonstrationPrefabCharacter;
    public GameObject PrefabCharacter => prefabCharacter;
    public Sprite SpriteCharacter => spriteCharacter;
    public string Name => nameCharacter;
    public string Description => descriptionCharacter;
    public CharacterStatData CharacterStat => characterStatData;
    public TypeAttackCharacter TypeAttack => typeAttack;
    public Color RarityColor => characterImprovementData.GetColor(rarityCharacter);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPerkData", menuName = "Perk")]
public class ItemPerkData : ScriptableObject
{
    [SerializeField] private string namePerk;
    [SerializeField] private string description;
    [SerializeField] private ItemPerk itemPerkComponent;
    public string NamePerk => namePerk;
    public string Description(int level)
    {
        return description + itemPerkComponent.GetValues()[level];
    }
}

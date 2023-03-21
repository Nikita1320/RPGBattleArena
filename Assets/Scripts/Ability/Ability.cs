using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private string nameAbility;
    [SerializeField] private string[] description;
    [SerializeField] private Sprite icon;
    [SerializeField] private int level;
    public virtual int MaxLevel { get; }
    public string NameAbility => nameAbility;
    public Sprite Icon => icon;
    public string GetDescription(int _level)
    {
        if (_level > 0 && _level < description.Length)
        {
            return description[_level];
        }
        return "";
    }
}

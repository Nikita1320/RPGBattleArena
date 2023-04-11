using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private string nameAbility;
    [SerializeField] private string[] description;
    [SerializeField] private Sprite icon;
    [SerializeField] protected int level;
    protected GameObject character;
    public string NameAbility => nameAbility;
    public Sprite Icon => icon;
    public int Level => level;
    public virtual string GetDescription(int _level)
    {
        if (_level > 0 && _level < description.Length)
        {
            return description[_level];
        }
        return "";
    }
    public virtual void Init(int level, GameObject character)
    {
        this.level = level;
        this.character = character;
    }
}

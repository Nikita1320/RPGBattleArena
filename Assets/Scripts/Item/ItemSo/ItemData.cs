using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private string description;
    [SerializeField] private string nameItem;

    public Sprite SpriteItem => sprite;
    public string Description => description;
    public string NameItem => nameItem;
}

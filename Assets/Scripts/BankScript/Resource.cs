using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChangeAmmount();
[System.Serializable]
public class Resource
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private Sprite resourceSprite;
    [SerializeField] private string nameResource;
    [SerializeField] private string descriptionResource;
    [SerializeField] private int ammount;
    public ChangeAmmount changeAmmountEvent;

    public ResourceType ResourceType => resourceType;
    public Sprite ResourceSprite => resourceSprite;
    public int Ammount => ammount;
    public string NameResource => nameResource;
    public string DescriptionResource => descriptionResource;
    public bool ChangeValue(int value)
    {
        if (ammount + value < 0)
        {
            return false;
        }
        ammount += value;
        changeAmmountEvent?.Invoke();
        return true;
    }
}

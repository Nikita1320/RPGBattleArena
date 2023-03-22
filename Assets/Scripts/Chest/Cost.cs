using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cost 
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int price;
    public int Price => price;
    public ResourceType ResourceType => resourceType;
}

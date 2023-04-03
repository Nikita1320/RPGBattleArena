using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceReward
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int ammount;
    public ResourceReward(ResourceType resourceType, int ammount)
    {
        this.resourceType = resourceType;
        this.ammount = ammount;
    }
    public ResourceType ResourceType => resourceType;
    public int Ammount => ammount;
}

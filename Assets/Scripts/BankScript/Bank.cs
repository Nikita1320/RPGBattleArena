using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType{
    ChestKey,
    Gold,
    Crystal
}
public class Bank : MonoBehaviour
{
    private static Bank instance;
    [SerializeField] private Resource[] resources;
    private bool isInitialized = false;
    private Dictionary<ResourceType, Resource> resourcesByType = new();
    public bool IsInitialized => isInitialized;
    public Dictionary<ResourceType, Resource> Resources => resourcesByType;
    public static Bank Instance => instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resourcesByType.Add((ResourceType)i, resources[i]);
        }
        isInitialized = true;
    }
}

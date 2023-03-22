using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPanel : MonoBehaviour
{
    [SerializeField] Bank bank;
    [SerializeField] private ResourceType[] dysplayedResources;
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private ResourceCell prefab;
    [SerializeField] private List<ResourceCell> resourceCells = new();
    private void Start()
    {
        bank = Bank.Instance;
        StartCoroutine(WaitInitializeBank());
    }
    private IEnumerator WaitInitializeBank()
    {
        while (true)
        {
            yield return null;
            Debug.Log("IWait");
            if (bank.IsInitialized == true)
            {
                InstanceCell();
                break;
            }
        }
    }
    private void InstanceCell()
    {
        foreach (var item in dysplayedResources)
        {
            var cell = Instantiate(prefab, contentPanel.transform);
            cell.Init(bank.Resources[item]);
            resourceCells.Add(cell);
        }
    }
}

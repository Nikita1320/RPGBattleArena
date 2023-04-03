using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsPanel : MonoBehaviour
{
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private List<GameObject> cells;
    [SerializeField] private EquipmentRewardCell equipmentRewardCellPrefab;
    [SerializeField] private CharacterCardRewardCell characterCardRewardCellPrefab;
    [SerializeField] private ResourceRewardCell resourceRewardCellPrefab;
    private Inventory inventory;
    private Bank bank;

    private void Start()
    {
        inventory = Inventory.Instance;
        bank = Bank.Instance;
    }
    public void OpenRewardPanel(EquipmentData[] equipments = null, CharacterCardData[] characterCards = null, ResourceReward[] resourceRewards = null)
    {
        gameObject.SetActive(true);
        if (inventory == null)
        {
            Start();
        }
        if (equipments != null)
        {
            foreach (var item in equipments)
            {
                inventory.AddItem(new Equipment(item));

                var cell = Instantiate(equipmentRewardCellPrefab, contentPanel.transform);
                cells.Add(cell.gameObject);
                cell.Init(item);
            }
        }
        if (characterCards != null)
        {
            foreach (var item in characterCards)
            {
                inventory.AddItem(new CharacterCard(item));

                var cell = Instantiate(characterCardRewardCellPrefab, contentPanel.transform);
                cells.Add(cell.gameObject);
                cell.Init(item);
            }
        }
        if (resourceRewards != null)
        {
            foreach (var item in resourceRewards)
            {
                bank.Resources[item.ResourceType].ChangeValue(item.Ammount);

                var cell = Instantiate(resourceRewardCellPrefab, contentPanel.transform);
                cells.Add(cell.gameObject);
                cell.Init(item);
            }
        }
    }
    public void ClosePanel()
    {
        foreach (var item in cells)
        {
            Destroy(item);
        }
        cells.Clear();
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestShop : MonoBehaviour
{
    [SerializeField] private Chest[] chest;
    [SerializeField] private Chest selectedChest;
    [SerializeField] private ChestDescription chestDescription;

    [SerializeField] private EquipmentRewardCell equipmentCellPrefab;
    [SerializeField] private CharacterCardRewardCell cardCellPrefab;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private GameObject rewardsConteiner;

    [SerializeField] private Transform pointSpawnChest;
    [SerializeField] private GameObject chestModel;
    [SerializeField] private Animator animator;

    private List<GameObject> rewardCells = new();
    private Inventory inventory;
    private void Start()
    {
        inventory = Inventory.Instance;
    }
    public void OpenChest(int ammountChestOpen)
    {
        if (selectedChest == null)
        {
            return;
        }
        rewardPanel.SetActive(true);
        selectedChest.GetRewards(ammountChestOpen, out List<Equipment> equipmentsRewards, out List<CharacterCard> characterCardRewards);

        Debug.Log(equipmentsRewards.Count);
        Debug.Log(characterCardRewards.Count);
        foreach (var item in equipmentsRewards)
        {
            var cell = Instantiate(equipmentCellPrefab, rewardsConteiner.transform);
            rewardCells.Add(cell.gameObject);
            cell.Init(item.EquipmentData);
        }
        foreach (var item in characterCardRewards)
        {
            var cell = Instantiate(cardCellPrefab, rewardsConteiner.transform);
            rewardCells.Add(cell.gameObject);
            cell.Init(item.CardData);
        }

        foreach (var item in equipmentsRewards)
        {
            inventory.AddItem(item);
        }
        foreach (var item in characterCardRewards)
        {
            inventory.AddItem(item);
        }
    }
    public void CloseRewardsPanel()
    {
        rewardPanel.SetActive(false);
        foreach (var item in rewardCells)
        {
            Destroy(item.gameObject);
        }
        rewardCells.Clear();
    }
    public void SelectChest(Chest selectedChest)
    {
        if (chestModel != null)
        {
            Destroy(chestModel);
        }
        this.selectedChest = selectedChest;
        chestModel = Instantiate(this.selectedChest.ChestData.ChestPrefab, pointSpawnChest);
        animator = chestModel.GetComponent<Animator>();
        chestDescription.gameObject.SetActive(true);
        chestDescription.Init(selectedChest);
    }
    public void OpenChestAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsOpen", true);
        }
    }
    public void CloseChestAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsOpen", false);
        }
    }
}

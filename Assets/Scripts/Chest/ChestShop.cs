using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChestShop : MonoBehaviour
{
    [SerializeField] private CamerasSwitcher camerasSwitcher;
    [SerializeField] private List<Camera> sceneCameras;

    [SerializeField] private Bank bank;
    [SerializeField] private Chest[] chest;
    [SerializeField] private Chest selectedChest;
    [SerializeField] private ChestDescription chestDescription;

    [SerializeField] private RewardsPanel rewardPanel;

    [SerializeField] private Transform pointSpawnChest;
    [SerializeField] private GameObject chestModel;
    [SerializeField] private Animator animator;

    [SerializeField] private PurchaseButton purchasebuttonPrefab;
    [SerializeField] private List<PurchaseButton> purchasebuttons = new();
    [SerializeField] private GameObject buttonsPanel;

    private List<GameObject> rewardCells = new();
    private Inventory inventory;
    private void Awake()
    {
        bank = Bank.Instance;
        inventory = Inventory.Instance;
    }
    public void OpenChest(Cost cost, int ammountChest)
    {
        if (!bank.Resources[cost.ResourceType].ChangeValue(-cost.Price * ammountChest))
        {
            return;
        }
        if (selectedChest == null)
        {
            return;
        }
        selectedChest.GetRewards(ammountChest, out List<EquipmentData> equipmentsRewards, out List<CharacterCardData> characterCardRewards);

        rewardPanel.OpenRewardPanel(equipmentsRewards.ToArray(), characterCardRewards.ToArray());

        foreach (var item in equipmentsRewards)
        {
            inventory.AddItem(new Equipment(item));
        }
        foreach (var item in characterCardRewards)
        {
            inventory.AddItem(new CharacterCard(item));
        }
    }

    public void SelectChest(Chest selectedChest)
    {
        if (this.selectedChest == selectedChest)
        {
            return;
        }
        if (chestModel != null)
        {
            Destroy(chestModel);
        }
        this.selectedChest = selectedChest;
        chestModel = Instantiate(this.selectedChest.ChestData.ChestPrefab, pointSpawnChest);
        animator = chestModel.GetComponent<Animator>();
        chestDescription.gameObject.SetActive(true);
        chestDescription.Init(selectedChest);
        if (purchasebuttons.Count != 0)
        {
            for (int i = 0; i < purchasebuttons.Count; i++)
            {
                Destroy(purchasebuttons[i].gameObject);
            }
            purchasebuttons.Clear();
        }

        for (int i = 0; i < selectedChest.ChestData.Costs.Length; i++)
        {
            InstancePurchaseButton(selectedChest.ChestData.Costs[i], 1);
            InstancePurchaseButton(selectedChest.ChestData.Costs[i], 5);
        }
    }

    private void InstancePurchaseButton(Cost cost, int chestAmmountOpen)
    {
        var button = Instantiate(purchasebuttonPrefab, buttonsPanel.transform);
        button.Init(bank.Resources[cost.ResourceType], cost.Price * chestAmmountOpen);
        purchasebuttons.Add(button);

        button.Button.onClick.AddListener(() => OpenChest(cost, chestAmmountOpen));
        button.onPointerEnter += OpenChestAnimation;
        button.onPointerExit += CloseChestAnimation;
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
    private void OnEnable()
    {
        camerasSwitcher.SwitchCamera(sceneCameras);
        SelectChest(chest[0]);
    }
}

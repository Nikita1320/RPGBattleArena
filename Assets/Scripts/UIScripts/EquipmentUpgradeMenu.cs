using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeImpoveEquipment
{
    Improve,
    Upgrade
}
public class EquipmentUpgradeMenu : MonoBehaviour
{
    [SerializeField] private EquipmentDescription equipmentDescription;
    [SerializeField] private EquipmentCell prefabCell;
    [SerializeField] private Sprite[] spriteWithTypeEquipment;

    [SerializeField] private Button applyImproveButton;
    [SerializeField] private GameObject equipmentsListPanel;
    [SerializeField] private List<EquipmentCell> equipmentsList;

    [SerializeField] private EquipmentCell[] equipmentCellsFirstLevel;
    [SerializeField] private EquipmentCell[] equipmentCellsSecondLevel;
    [SerializeField] private EquipmentCell[] equipmentCellsThirdLevel;
    [SerializeField] private EquipmentCell[] equipmentCellsFourthLevel;
    [SerializeField] private EquipmentCell[] equipmentCellsFifthLevel;

    [SerializeField] private GameObject panelFirstLevel;
    [SerializeField] private GameObject panelSecondLevel;
    [SerializeField] private GameObject panelThirdLevel;
    [SerializeField] private GameObject panelFourthLevel;
    [SerializeField] private GameObject panelFifthLevel;

    private GameObject currentOpenPanel;
    private Dictionary<int, EquipmentCell[]> cellsWithLevel = new();
    private Dictionary<int, GameObject> panelWithLevel = new();

    private Equipment impovingEquipment;
    private TypeImpoveEquipment currentTypeImprove;
    private int targetLevel;
    private Inventory inventory;

    private void Init()
    {
        inventory = Inventory.Instance;
        //Подписка на добавление/удаление ячеек(возможно не нужно)
        cellsWithLevel.Add(0, equipmentCellsFirstLevel);
        cellsWithLevel.Add(1, equipmentCellsSecondLevel);
        cellsWithLevel.Add(2, equipmentCellsThirdLevel);
        cellsWithLevel.Add(3, equipmentCellsFourthLevel);
        cellsWithLevel.Add(4, equipmentCellsFifthLevel);

        panelWithLevel.Add(0, panelFirstLevel);
        panelWithLevel.Add(1, panelSecondLevel);
        panelWithLevel.Add(2, panelThirdLevel);
        panelWithLevel.Add(3, panelFourthLevel);
        panelWithLevel.Add(4, panelFifthLevel);
    }

    public void OpenUpgradePanel(Equipment equipment, TypeImpoveEquipment typeImpove)
    {
        ResetMenu();

        if (cellsWithLevel.Count == 0)
        {
            Init();
        }
        impovingEquipment = equipment;
        currentTypeImprove = typeImpove;

        if (currentTypeImprove == TypeImpoveEquipment.Improve)
        {
            targetLevel = equipment.CurrentLevelPerk;
            equipmentDescription.Init(impovingEquipment, true,false);
        }
        else
        {
            targetLevel = equipment.CurrentLevelStats;
            equipmentDescription.Init(impovingEquipment, false,true);
        }

        if (targetLevel < equipment.MaxLevelStatsUpgrade)
        {
            applyImproveButton.interactable = true;

            ResetSelectionCells(cellsWithLevel[targetLevel]);
            currentOpenPanel = panelWithLevel[targetLevel];
            currentOpenPanel.SetActive(true);
        }
        else
        {
            applyImproveButton.interactable = false;
        }
        InstantiateCells();
        this.gameObject.SetActive(true);
    }

    public void InstantiateCells()
    {
        Equipment[] equipments;

        if (currentTypeImprove == TypeImpoveEquipment.Improve)
        {
            equipments = inventory.GetAllSameEquipment(impovingEquipment.EquipmentData, impovingEquipment);
        }
        else
        {
            equipments = inventory.GetEquipments(impovingEquipment.EquipmentData.EquipmentType, impovingEquipment.EquipmentData.Rare, impovingEquipment);
            Debug.Log(equipments.Length);
        }

        for (int i = 0; i < equipments.Length; i++)
        {
            InstanceCell(equipments[i]);
        }
        return;
    }

    private void InstanceCell(Equipment equipment)
    {
        var cell = Instantiate(prefabCell, equipmentsListPanel.transform);
        equipmentsList.Add(cell);
        cell.Init(equipment);
        cell.CellButton.onClick.AddListener(() => SelectEquipment(cell));
    }

    public void SelectEquipment(EquipmentCell selectedCell)
    {
        if (FindEmptyCell(out EquipmentCell emptyCell))
        {
            emptyCell.Init(selectedCell.Equipment);

            equipmentsList.Remove(selectedCell);
            Destroy(selectedCell.gameObject);
        }
    }

    public void ClearSelectionCell(EquipmentCell equipmentCell)
    {
        InstanceCell(equipmentCell.Equipment);

        equipmentCell.Init(null);
        equipmentCell.RareImage.color = new Color(0, 0, 0, 0);
        equipmentCell.ItemImage.sprite = spriteWithTypeEquipment[(int)impovingEquipment.EquipmentData.EquipmentType];
    }

    public void ApplyUpgrade()
    {
        if (currentTypeImprove == TypeImpoveEquipment.Improve)
        {
            if (impovingEquipment.CurrentLevelPerk == impovingEquipment.CurrentLevelStats)
            {
                return;
            }
        }
        foreach (var item in cellsWithLevel[targetLevel])
        {
            if (item.Equipment == null)
            {
                return;
            }
        }

        foreach (var item in cellsWithLevel[targetLevel])
        {
            inventory.RemoveEquipment(item.Equipment);
        }

        ResetSelectionCells(cellsWithLevel[targetLevel]);

        panelWithLevel[targetLevel].SetActive(false);

        if (currentTypeImprove == TypeImpoveEquipment.Improve)
        {
            impovingEquipment.UpgradePerk();
        }
        else
        {
            impovingEquipment.UpgradeStats();
        }

        targetLevel++;

        if (targetLevel < impovingEquipment.MaxLevelPerkUpgrade)
        {
            currentOpenPanel = panelWithLevel[targetLevel];
            currentOpenPanel.SetActive(true);
            ResetSelectionCells(cellsWithLevel[targetLevel]);
        }
        else
        {
            applyImproveButton.interactable = false;
        }
    }

    public void ResetSelectionCells(EquipmentCell[] equipmentCells)
    {
        foreach (var item in equipmentCells)
        {
            item.Init(null);
            item.RareImage.color = new Color(0, 0, 0, 0);
            item.ItemImage.sprite = spriteWithTypeEquipment[(int)impovingEquipment.EquipmentData.EquipmentType];
        }
    }
    public void ClearEquipmentsPanel()
    {
        foreach (var item in equipmentsList)
        {
            Destroy(item.gameObject);
        }
    }
    private bool FindEmptyCell(out EquipmentCell equipmentCell)
    {
        foreach (var item in cellsWithLevel[targetLevel])
        {
            if (item.Equipment == null)
            {
                equipmentCell = item;
                return true;
            }
        }
        equipmentCell = null;
        return false;
    }

    public void ResetMenu()
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
        }

        ClearEquipmentsPanel();

        equipmentsList.Clear();

        equipmentDescription.Init(null);

        targetLevel = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectionPanel : MonoBehaviour
{
    private Inventory inventory;
    private TypeEquipment currentTypeSelectedEquipment;
    private Equipment clothedEquipment;
    private EquipmentCell selectedEquipmentCell;
    private Character character;

    [SerializeField] private GameObject handListItemsPanel;
    [SerializeField] private GameObject bodyListItemsPanel;
    [SerializeField] private GameObject headListItemsPanel;
    [SerializeField] private GameObject footListItemsPanel;
    [SerializeField] private List<EquipmentCell> handEquipmentCells = new();
    [SerializeField] private List<EquipmentCell> bodyEquipmentCells = new();
    [SerializeField] private List<EquipmentCell> headEquipmentCells = new();
    [SerializeField] private List<EquipmentCell> footEquipmentCells = new();
    private GameObject openList;
    [SerializeField] private EquipmentDescription selectedEquipmentDescription;
    [SerializeField] private EquipmentDescription currentEquipmentDescription;
    [SerializeField] private EquipmentCell prefabEquipmentCell;
    [SerializeField] private Button[] equipmentButton;

    private void Start()
    {
        gameObject.SetActive(false);
        inventory = Inventory.Instance;
        inventory.AddedEquipmentEvent += AddEquipmentCell;
        inventory.RemovedEquipmentEvent += RemoveEquipmentCell;
        InstanceEquipmentCells();
    }
    public void Init(Character _character)
    {
        character = _character;
    }
    public void OpenEquipmentsList(TypeEquipment typeEquipment)
    {
        if (typeEquipment == TypeEquipment.Hand)
        {
            if (openList != null)
            {
                openList.SetActive(false);
            }
            openList = handListItemsPanel.transform.parent.gameObject;
            openList.SetActive(true);
            OpenDescriptionCurrentEquipment(typeEquipment);
        }
        else if (typeEquipment == TypeEquipment.Body)
        {
            if (openList != null)
            {
                openList.SetActive(false);
            }
            openList = bodyListItemsPanel.transform.parent.gameObject;
            openList.SetActive(true);

            OpenDescriptionCurrentEquipment(typeEquipment);
        }
        else if (typeEquipment == TypeEquipment.Head)
        {
            if (openList != null)
            {
                openList.SetActive(false);
            }
            openList = headListItemsPanel.transform.parent.gameObject;
            openList.SetActive(true);

            OpenDescriptionCurrentEquipment(typeEquipment);
        }
        else if (typeEquipment == TypeEquipment.Foot)
        {
            if (openList != null)
            {
                openList.SetActive(false);
            }
            openList = footListItemsPanel.transform.parent.gameObject;
            openList.SetActive(true);

            OpenDescriptionCurrentEquipment(typeEquipment);
        }
        selectedEquipmentDescription.gameObject.SetActive(false);
        currentTypeSelectedEquipment = typeEquipment;
    }
    private void AddEquipmentCell(Equipment equipment)
    {
        if (equipment.EquipmentData.EquipmentType == TypeEquipment.Hand)
        {
            var cell = Instantiate(prefabEquipmentCell, handListItemsPanel.transform);
            handEquipmentCells.Add(cell);
            cell.Init(equipment);

            cell.CellButton.onClick.AddListener(() => OpenDescriptionSelectedEquipment(cell));
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Body)
        {
            var cell = Instantiate(prefabEquipmentCell, bodyListItemsPanel.transform);
            bodyEquipmentCells.Add(cell);
            cell.Init(equipment);

            cell.CellButton.onClick.AddListener(() => OpenDescriptionSelectedEquipment(cell));
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Head)
        {
            var cell = Instantiate(prefabEquipmentCell, headListItemsPanel.transform);
            headEquipmentCells.Add(cell);
            cell.Init(equipment);

            cell.CellButton.onClick.AddListener(() => OpenDescriptionSelectedEquipment(cell));
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Foot)
        {
            var cell = Instantiate(prefabEquipmentCell, footListItemsPanel.transform);
            footEquipmentCells.Add(cell);
            cell.Init(equipment);

            cell.CellButton.onClick.AddListener(() => OpenDescriptionSelectedEquipment(cell));
        }
    }
    public void RemoveEquipmentCell(Equipment equipment)
    {
        if (equipment.EquipmentData.EquipmentType == TypeEquipment.Hand)
        {
            foreach (var item in handEquipmentCells)
            {
                if (item.Equipment == equipment)
                {
                    handEquipmentCells.Remove(item);
                    Destroy(item.gameObject);
                    return;
                }
            }
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Body)
        {
            foreach (var item in bodyEquipmentCells)
            {
                if (item.Equipment == equipment)
                {
                    handEquipmentCells.Remove(item);
                    Destroy(item.gameObject);
                    return;
                }
            }
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Head)
        {
            foreach (var item in headEquipmentCells)
            {
                if (item.Equipment == equipment)
                {
                    handEquipmentCells.Remove(item);
                    Destroy(item.gameObject);
                    return;
                }
            }
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Foot)
        {
            foreach (var item in footEquipmentCells)
            {
                if (item.Equipment == equipment)
                {
                    handEquipmentCells.Remove(item);
                    Destroy(item.gameObject);
                    return;
                }
            }
        }
    }
    private void InstanceEquipmentCells()
    {
        for (int i = 0; i < inventory.Equipments.Length; i++)
        {
            AddEquipmentCell(inventory.Equipments[i]);
        }
    }

    private void OpenDescriptionSelectedEquipment(EquipmentCell equipmentCell)
    {
        selectedEquipmentDescription.gameObject.SetActive(true);
        selectedEquipmentDescription.Init(equipmentCell.Equipment);
        selectedEquipmentCell = equipmentCell;
    }

    private void OpenDescriptionCurrentEquipment(TypeEquipment typeEquipment)
    {
        clothedEquipment = character.Equipments[typeEquipment];

        if (clothedEquipment != null)
        {
            currentEquipmentDescription.Init(character.Equipments[typeEquipment]);
            currentEquipmentDescription.gameObject.SetActive(true);
        }
        else
        {
            currentEquipmentDescription.gameObject.SetActive(false);
        }
    }

    public void ClothEquipment()
    {
        if (clothedEquipment != null)
        {
            character.RemoveEquipment(clothedEquipment);
        }

        if (selectedEquipmentCell.Equipment.Owner != null)
        {
            selectedEquipmentCell.Equipment.Owner.RemoveEquipment(selectedEquipmentCell.Equipment);
        }

        character.Equip(selectedEquipmentCell.Equipment);
        selectedEquipmentDescription.gameObject.SetActive(false);
        OpenDescriptionCurrentEquipment(currentTypeSelectedEquipment);

        selectedEquipmentCell = null;
    }

    public void TakeOffEquipment()
    {
        currentEquipmentDescription.gameObject.SetActive(false);

        var removedEquipment = character.Equipments[currentTypeSelectedEquipment];
        character.RemoveEquipment(removedEquipment);
        clothedEquipment = null;
    }
    public void ResetPanel()
    {
        inventory.AddedEquipmentEvent -= AddEquipmentCell;
        inventory.RemovedEquipmentEvent -= RemoveEquipmentCell;
        character = null;
    }
}

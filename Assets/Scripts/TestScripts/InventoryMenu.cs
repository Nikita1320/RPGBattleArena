using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private ItemDescriptionPanel descriptionPanel;
    [SerializeField] private GameObject handEquipmentPanel;
    [SerializeField] private GameObject bodyEquipmentPanel;
    [SerializeField] private GameObject headEquipmentPanel;
    [SerializeField] private GameObject footEquipmentPanel;
    [SerializeField] private GameObject cardPanel;

    [SerializeField] private EquipmentCell EquipmentCellPrefab;
    [SerializeField] private CharacterCardCell CardCellPrefab;

    [SerializeField] private List<EquipmentCell> handEquipmentCells;
    [SerializeField] private List<EquipmentCell> bodyEquipmentCells;
    [SerializeField] private List<EquipmentCell> headEquipmentCells;
    [SerializeField] private List<EquipmentCell> footEquipmentCells;
    [SerializeField] private List<CharacterCardCell> characterCardCells;

    private void Start()
    {
        inventory = Inventory.Instance;

        for (int i = 0; i < inventory.Equipments.Length; i++)
        {
            InstantiateEquipmentCell(inventory.Equipments[i]);
        }
        for (int i = 0; i < inventory.CharacterCards.Length; i++)
        {
            InstantiateCardCell(inventory.CharacterCards[i]);
        }

        inventory.AddedEquipmentEvent += InstantiateEquipmentCell;
        inventory.AddedCardtEvent += InstantiateCardCell;
    }
    public void InstantiateEquipmentCell(Equipment equipment)
    {
        if (equipment.EquipmentData.EquipmentType == TypeEquipment.Hand)
        {
            handEquipmentCells.Add(Instantiate(EquipmentCellPrefab, handEquipmentPanel.transform));
            handEquipmentCells[handEquipmentCells.Count - 1].Init(equipment);
            handEquipmentCells[handEquipmentCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(equipment));

            equipment.removingEvent += () => RemoveEquipmentCell(handEquipmentCells[handEquipmentCells.Count - 1]);
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Body)
        {
            bodyEquipmentCells.Add(Instantiate(EquipmentCellPrefab, bodyEquipmentPanel.transform));
            bodyEquipmentCells[bodyEquipmentCells.Count - 1].Init(equipment);
            bodyEquipmentCells[bodyEquipmentCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(equipment));

            equipment.removingEvent += () => RemoveEquipmentCell(handEquipmentCells[handEquipmentCells.Count - 1]);
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Head)
        {
            headEquipmentCells.Add(Instantiate(EquipmentCellPrefab, headEquipmentPanel.transform));
            headEquipmentCells[headEquipmentCells.Count - 1].Init(equipment);
            headEquipmentCells[headEquipmentCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(equipment));

            equipment.removingEvent += () => RemoveEquipmentCell(handEquipmentCells[handEquipmentCells.Count - 1]);
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Foot)
        {
            footEquipmentCells.Add(Instantiate(EquipmentCellPrefab, footEquipmentPanel.transform));
            footEquipmentCells[footEquipmentCells.Count - 1].Init(equipment);
            footEquipmentCells[footEquipmentCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(equipment));

            equipment.removingEvent += () => RemoveEquipmentCell(handEquipmentCells[handEquipmentCells.Count - 1]);
        }
    }
    public void InstantiateCardCell(CharacterCard characterCard)
    {
        characterCardCells.Add(Instantiate(CardCellPrefab, cardPanel.transform));
        characterCardCells[characterCardCells.Count - 1].Init(characterCard);

        characterCardCells[characterCardCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(characterCard));

        characterCardCells[characterCardCells.Count - 1].CharacterCard.RemovingEvent += () => RemoveCardCell(characterCardCells[characterCardCells.Count]);
    }
    public void RemoveEquipmentCell(EquipmentCell equipmentCell)
    {
        if (equipmentCell.Equipment.EquipmentData.EquipmentType == TypeEquipment.Hand)
        {
            handEquipmentCells.Remove(equipmentCell);
            Destroy(equipmentCell);
        }
        else if (equipmentCell.Equipment.EquipmentData.EquipmentType == TypeEquipment.Body)
        {
            bodyEquipmentCells.Remove(equipmentCell);
            Destroy(equipmentCell);
        }
        else if (equipmentCell.Equipment.EquipmentData.EquipmentType == TypeEquipment.Head)
        {
            headEquipmentCells.Remove(equipmentCell);
            Destroy(equipmentCell);
        }
        else if (equipmentCell.Equipment.EquipmentData.EquipmentType == TypeEquipment.Foot)
        {
            footEquipmentCells.Remove(equipmentCell);
            Destroy(equipmentCell);
        }
    }
    public void RemoveCardCell(CharacterCardCell characterCardCell)
    {
        characterCardCells.Remove(characterCardCell);
        Destroy(characterCardCell.gameObject);
    }
}

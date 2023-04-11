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
        inventory.RemovedEquipmentEvent += RemoveEquipmentCell;
        inventory.RemovedCardEvent += RemoveCardCell;
    }
    public void InstantiateEquipmentCell(Equipment equipment)
    {
        if (equipment.EquipmentData.EquipmentType == TypeEquipment.Hand)
        {
            handEquipmentCells.Add(Instantiate(EquipmentCellPrefab, handEquipmentPanel.transform));
            handEquipmentCells[handEquipmentCells.Count - 1].Init(equipment);
            handEquipmentCells[handEquipmentCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(equipment));
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Body)
        {
            bodyEquipmentCells.Add(Instantiate(EquipmentCellPrefab, bodyEquipmentPanel.transform));
            bodyEquipmentCells[bodyEquipmentCells.Count - 1].Init(equipment);
            bodyEquipmentCells[bodyEquipmentCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(equipment));
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Head)
        {
            headEquipmentCells.Add(Instantiate(EquipmentCellPrefab, headEquipmentPanel.transform));
            headEquipmentCells[headEquipmentCells.Count - 1].Init(equipment);
            headEquipmentCells[headEquipmentCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(equipment));
        }
        else if (equipment.EquipmentData.EquipmentType == TypeEquipment.Foot)
        {
            footEquipmentCells.Add(Instantiate(EquipmentCellPrefab, footEquipmentPanel.transform));
            footEquipmentCells[footEquipmentCells.Count - 1].Init(equipment);
            footEquipmentCells[footEquipmentCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(equipment));
        }
    }
    public void InstantiateCardCell(CharacterCard characterCard)
    {
        characterCardCells.Add(Instantiate(CardCellPrefab, cardPanel.transform));
        characterCardCells[characterCardCells.Count - 1].Init(characterCard);

        characterCardCells[characterCardCells.Count - 1].CellButton.onClick.AddListener(() => descriptionPanel.RenderDescription(characterCard));
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
    public void RemoveCardCell(CharacterCard characterCard)
    {
        foreach (var item in characterCardCells)
        {
            if (item.CharacterCard == characterCard)
            {
                characterCardCells.Remove(item);
                Destroy(item.gameObject);
                return;
            }
        }
    }
    private void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.AddedEquipmentEvent -= InstantiateEquipmentCell;
            inventory.AddedCardtEvent -= InstantiateCardCell;
            inventory.RemovedEquipmentEvent -= RemoveEquipmentCell;
            inventory.RemovedCardEvent -= RemoveCardCell;
        }
    }
}

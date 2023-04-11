using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventoryPanel : MonoBehaviour
{
    [SerializeField] EquipmentSelectionPanel equipmentSelectionPanel;
    [SerializeField] private EquipmentCell[] equipmentCell;
    [SerializeField] private Button[] buttons;
    private Character character;

    private void Start()
    {
        buttons[0].onClick.AddListener(() => equipmentSelectionPanel.OpenEquipmentsList(TypeEquipment.Hand));
        buttons[1].onClick.AddListener(() => equipmentSelectionPanel.OpenEquipmentsList(TypeEquipment.Body));
        buttons[2].onClick.AddListener(() => equipmentSelectionPanel.OpenEquipmentsList(TypeEquipment.Head));
        buttons[3].onClick.AddListener(() => equipmentSelectionPanel.OpenEquipmentsList(TypeEquipment.Foot));
    }
    public void Init(Character _character)
    {
        if (character != null)
        {
            character.reachedFirstRank -= SetEquipmentForCell;
            character.equipedItemEvent -= UpdateEquipmendCell;
            character.removedItemEvent -= UpdateEquipmendCell;
        }

        character = _character;

        character.reachedFirstRank += SetEquipmentForCell;
        character.equipedItemEvent += UpdateEquipmendCell;
        character.removedItemEvent += UpdateEquipmendCell;

        if (character.Rank < 1)
        {
            character.reachedFirstRank += SetEquipmentForCell;
        }
        else
        {
            SetEquipmentForCell();
        }
        equipmentSelectionPanel.Init(character);
    }
    private void SetEquipmentForCell()
    {
        for (int i = 0; i < character.Equipments.Count; i++)
        {
            equipmentCell[i].Init(character.Equipments[(TypeEquipment)i]);
        }
        if (character.IsOpen)
        {
            for (int i = 0; i < character.Equipments.Count; i++)
            {
                buttons[i].interactable = true;
                if (character.Equipments[(TypeEquipment)i] != null)
                {
                    equipmentCell[i].gameObject.SetActive(true);
                }
                else
                {
                    equipmentCell[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < equipmentCell.Length; i++)
            {
                equipmentCell[i].gameObject.SetActive(false);
                buttons[i].interactable = false;
            }
        }
    }
    private void UpdateEquipmendCell(TypeEquipment typeEquipment)
    {
        equipmentCell[(int)typeEquipment].Init(character.Equipments[typeEquipment]);

        if (character.Equipments[typeEquipment] != null)
        {
            equipmentCell[(int)typeEquipment].gameObject.SetActive(true);
        }
        else
        {
            equipmentCell[(int)typeEquipment].gameObject.SetActive(false);
        }
    }
    public void ResetPanel()
    {
        character.reachedFirstRank -= SetEquipmentForCell;
        character.equipedItemEvent -= UpdateEquipmendCell;
        character.removedItemEvent -= UpdateEquipmendCell;
        character = null;
        equipmentSelectionPanel.ResetPanel();
    }
}

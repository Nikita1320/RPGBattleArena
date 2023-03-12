using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentCell : ItemCell
{
    [SerializeField] private Color[] rareEquipmentColor;
    [SerializeField] private Image ownerImage;
    [SerializeField] private Image ownerPanel;
    [SerializeField] private EquipmentImprovementPanel improvementPanel;

    private Equipment equipment;

    public Equipment Equipment => equipment;
    public void Init(Equipment _equipment)
    {
        if (equipment != null)
        {
            equipment.changedOwnerEvent -= UpdateOwnerImage;
        }

        equipment = _equipment;
        improvementPanel.Init(equipment);

        if (equipment != null)
        {
            itemImage.sprite = equipment.EquipmentData.SpriteItem;
            rareImage.color = rareEquipmentColor[(int)equipment.EquipmentData.Rare];

            if (ownerPanel != null)
            {
                if (equipment.Owner != null)
                {
                    ownerPanel.gameObject.SetActive(true);
                    ownerImage.sprite = equipment.Owner.CharacterData.SpriteCharacter;
                }
                else
                {
                    ownerPanel.gameObject.SetActive(false);
                }
                equipment.changedOwnerEvent += UpdateOwnerImage;
            }
        }
    }
    private void UpdateOwnerImage()
    {
        if (equipment.Owner == null)
        {
            ownerPanel.gameObject.SetActive(false);
        }
        else
        {
            ownerPanel.gameObject.SetActive(true);
            ownerImage.sprite = equipment.Owner.CharacterData.SpriteCharacter;
        }
    }
    private void OnDestroy()
    {
        if (equipment != null)
        {
            equipment.changedOwnerEvent -= UpdateOwnerImage;
        }
    }
}

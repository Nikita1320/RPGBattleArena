using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField] private EquipmentDescription equipmentDescriptionPanel;
    [SerializeField] private CharacterCardDescription cardDescription;

    public void RenderDescription(Equipment equipment)
    {
        cardDescription.gameObject.SetActive(false);

        equipmentDescriptionPanel.Init(equipment);
        equipmentDescriptionPanel.gameObject.SetActive(true);
    }
    public void RenderDescription(CharacterCard characterCard)
    {
        equipmentDescriptionPanel.gameObject.SetActive(false);

        cardDescription.gameObject.SetActive(true);
        cardDescription.Init(characterCard);
    }
    private void OnDisable()
    {
        cardDescription.gameObject.SetActive(false);
        equipmentDescriptionPanel.gameObject.SetActive(false);
    }
}

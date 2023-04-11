using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPerkPanel : MonoBehaviour
{
    private Equipment equipment;
    [SerializeField] private Text perkNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text descriptionTextNextLevel;
    private bool renderNextLevelPerkDescription;
    public void Init(Equipment _equipment, bool renderNextLevelPerkDescription)
    {
        if (equipment != null)
        {
            equipment.improvedPerkEvent -= RenderPerkInfo;
        }
        equipment = _equipment;

        this.renderNextLevelPerkDescription = renderNextLevelPerkDescription;

        RenderPerkInfo();

        equipment.improvedPerkEvent += RenderPerkInfo;
    }
    public void RenderPerkInfo()
    {
        perkNameText.text = equipment.EquipmentPerk.NamePerk;
        descriptionText.text = "Current Boost: " + equipment.EquipmentPerk.Description(equipment.CurrentLevelPerk);

        if (renderNextLevelPerkDescription)
        {
            if (equipment.CurrentLevelPerk < equipment.MaxLevelPerkUpgrade)
            {
                descriptionText.text += "\nBoost On Next Level: " + equipment.EquipmentPerk.Description(equipment.CurrentLevelPerk + 1);
            }
        }
    }
    private void OnDestroy()
    {
        if (equipment != null)
        {
            equipment.improvedPerkEvent -= RenderPerkInfo;
        }
    }
}

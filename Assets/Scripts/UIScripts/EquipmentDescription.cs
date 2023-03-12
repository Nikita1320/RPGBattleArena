using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDescription : MonoBehaviour
{
    private Equipment equipment;

    [SerializeField] private Text nameEquipment;
    [SerializeField] private Text nameOwner;

    [SerializeField] private EquipmentCell equipmentCell;
    [SerializeField] private EquipmentStatsPanel statsPanel;
    [SerializeField] private EquipmentPerkPanel perkPanel;
    [SerializeField] private bool renderNextLevelPerkDescription = false;
    [SerializeField] private bool renderNextLevelStat = false;

    public EquipmentStatsPanel StatsPanel => statsPanel;
    public EquipmentPerkPanel PerkPanel => perkPanel;

    public void Init(Equipment _equipment, bool renderNextLevelPerkDescription = false, bool renderNextLevelStat = false)
    {
        if (_equipment != equipment)
        {
            if (equipment != null)
            {
                equipment.improvedStatsEvent -= RenderPerkPanel;

                equipment.improvedStatsEvent -= RenderStatPanel;
            }
            equipment = _equipment;

            nameEquipment.text = equipment.EquipmentData.NameItem;

            equipmentCell.gameObject.SetActive(true);
            equipmentCell.Init(equipment);

            RenderPerkPanel();
            RenderStatPanel();

            equipment.improvedPerkEvent += RenderPerkPanel;

            equipment.improvedPerkEvent += RenderStatPanel;
        }
    }
    private void RenderPerkPanel()
    {
        if (perkPanel)
        {
            perkPanel.gameObject.SetActive(true);
            perkPanel.Init(equipment,renderNextLevelPerkDescription);
        }
    }
    private void RenderStatPanel()
    {
        if (statsPanel)
        {
            statsPanel.gameObject.SetActive(true);
            statsPanel.Init(equipment,renderNextLevelStat);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentStatsPanel : MonoBehaviour
{
    [SerializeField] private List<EquipmentStatCell> statCells;
    [SerializeField] private EquipmentStatCell prefabStatCell;
    private bool renderNextLevelStat = false;
    private Equipment equipment;
    public void Init(Equipment _equipment, bool renderNextLevelStat)
    {
        if (equipment != null)
        {
            equipment.improvedStatsEvent -= RenderStat;
        }
        equipment = _equipment;

        this.renderNextLevelStat = renderNextLevelStat;

        InstanceStatCell();
        RenderStat();

        equipment.improvedStatsEvent += RenderStat;
    }
    private void InstanceStatCell()
    {
        for (int i = 0; i < statCells.Count; i++)
        {
            Destroy(statCells[i].gameObject);
        }

        statCells.Clear();

        for (int i = 0; i < equipment.EquipmentData.EquipmentStat.Length; i++)
        {
            statCells.Add(Instantiate(prefabStatCell, transform));
        }
    }
    private void DestroyCell()
    {
        for (int i = equipment.EquipmentData.EquipmentStat.Length; i < statCells.Count; i++)
        {
            var temp = statCells[i];
            statCells.Remove(statCells[i]);
            Destroy(statCells[i].gameObject);
        }
    }
    private void RenderStat()
    {
        for (int i = 0; i < equipment.EquipmentData.EquipmentStat.Length; i++)
        {
            statCells[i].Init(equipment.EquipmentData.EquipmentStat[i], equipment.CurrentLevelStats, renderNextLevelStat);
        }
    }
}

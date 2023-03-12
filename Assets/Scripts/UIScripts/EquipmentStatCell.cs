using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentStatCell : MonoBehaviour
{
    [SerializeField] private Text nameStat;
    [SerializeField] private Text statValue;
    [SerializeField] private Text addedValueOnUpLevel;

    public void Init(EquipmentStat equipmentStat, int level, bool renderNextValue)
    {
        nameStat.text = equipmentStat.TypeStat.ToString() + ": ";
        statValue.text = equipmentStat.GetValue(level).ToString();
        if (equipmentStat.TypeValue == TypeValueStat.Percent)
        {
            statValue.text = equipmentStat.GetValue(level).ToString() + "%";
        }
        else
        {
            statValue.text = equipmentStat.GetValue(level).ToString();
        }
        if (renderNextValue)
        {
            if (level < 4)
            {
                if (equipmentStat.TypeValue == TypeValueStat.Percent)
                {
                    addedValueOnUpLevel.text = "+ " + (equipmentStat.GetValue(level + 1) - equipmentStat.GetValue(level)).ToString() + "%";
                }
                else
                {
                    addedValueOnUpLevel.text = "+ " + (equipmentStat.GetValue(level + 1) - equipmentStat.GetValue(level)).ToString();
                }
            }
            else
            {
                addedValueOnUpLevel.text = "";
            }
        }
        else
        {
            addedValueOnUpLevel.text = "";
        }
    }
}

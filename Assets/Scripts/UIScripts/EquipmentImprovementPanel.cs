using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentImprovementPanel : MonoBehaviour
{
    [SerializeField] private List<Image> stars;
    [SerializeField] private Sprite improvedStar;
    [SerializeField] private Sprite notImprvedStarSprite;
    private Equipment equipment;

    public void Init(Equipment _equipment)
    {
        if (equipment != null)
        {
            equipment.improvedPerkEvent -= UpdateImageImprovementPerk;
            equipment.improvedStatsEvent -= UpdateImageImprovementStats;
        }

        equipment = _equipment;

        if (equipment != null)
        {
            UpdateImageImprovementStats();
            UpdateImageImprovementPerk();

            equipment.improvedPerkEvent += UpdateImageImprovementPerk;
            equipment.improvedStatsEvent += UpdateImageImprovementStats;
        }
        else
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
    }
    private void UpdateImageImprovementStats()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (equipment.CurrentLevelStats > i)
            {
                stars[i].gameObject.SetActive(true);
            }
            else
            {
                stars[i].gameObject.SetActive(false);
            }
        }
    }
    private void UpdateImageImprovementPerk()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (equipment.CurrentLevelPerk > i)
            {
                stars[i].sprite = improvedStar;
            }
            else
            {
                stars[i].sprite = notImprvedStarSprite;
            }
        }
    }
    private void OnDestroy()
    {
        if (equipment != null)
        {
            equipment.improvedPerkEvent -= UpdateImageImprovementPerk;
            equipment.improvedStatsEvent -= UpdateImageImprovementStats;
        }
    }
}

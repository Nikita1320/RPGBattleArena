using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMenu : MonoBehaviour
{
    [SerializeField] private CharacterInformationPanel informationPanel;
    [SerializeField] private EquipmentSelectionPanel equipmentSelectionPanel;
    private void OnDisable()
    {
        informationPanel.Reset();
        equipmentSelectionPanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        
    }
}

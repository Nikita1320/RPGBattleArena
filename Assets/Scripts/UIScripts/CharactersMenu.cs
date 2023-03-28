using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMenu : MonoBehaviour
{
    [SerializeField] private CharacterInformationPanel informationPanel;
    [SerializeField] private EquipmentSelectionPanel equipmentSelectionPanel;
    [SerializeField] private CamerasSwitcher camerasSwitcher;
    [SerializeField] private List<Camera> sceneCameras;
    private void OnDisable()
    {
        informationPanel.Reset();
        equipmentSelectionPanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        camerasSwitcher.SwitchCamera(sceneCameras);
    }
}

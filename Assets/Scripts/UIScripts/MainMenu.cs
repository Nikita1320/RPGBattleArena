using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CamerasSwitcher camerasSwitcher;
    [SerializeField] private List<Camera> sceneCameras;
    [SerializeField] private Transform spawnPoint;
    private Character character;
    private GameObject demonstrationPrefab;
    private CharactersManager charactersManager;
    private GameObject selectedCharacter;

    private void Start()
    {
        charactersManager = CharactersManager.Instance;
        charactersManager.changedCharacter += InstantiateDemonstrationCharacter;
    }

    public void InstantiateDemonstrationCharacter(Character _character)
    {
        if (_character != character)
        {
            if (demonstrationPrefab != null)
            {
                Destroy(demonstrationPrefab);
            }
            character = _character;

            demonstrationPrefab = Instantiate(character.CharacterData.DemonstrationPrefabCharacter,spawnPoint);
            demonstrationPrefab.transform.localPosition = Vector3.zero;
        }
    }
    private void OnEnable()
    {
        camerasSwitcher.SwitchCamera(sceneCameras);
        spawnPoint.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        if (spawnPoint != null)
        {
            spawnPoint.gameObject.SetActive(false);
        }
    }
}

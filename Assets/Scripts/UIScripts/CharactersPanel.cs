using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersPanel : MonoBehaviour
{
    [SerializeField] private CharacterInformationPanel informationPanel;
    [SerializeField] private Button selectedCharacterButton;
    [SerializeField] private EquipmentSelectionPanel equipmentSelectionPanel;
    
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private CharacterUICell prefab;

    [SerializeField] private GameObject commonCharacterCellsConteiner;
    [SerializeField] private GameObject epicCharacterCellsConteiner;
    [SerializeField] private GameObject legendCharacterCellsConteiner;

    private CharactersManager charactersManager;
    private CharacterUICell selectedCell;
    private GameObject demoCharacterGameObject;

    private List<CharacterUICell> commonCharacterCells = new();
    private List<CharacterUICell> epicCharacterCells = new();
    private List<CharacterUICell> legendaryCharacterCells = new();

    public Character DemonstrationCharacter => selectedCell.Character;

    private void Start()
    {
        charactersManager = CharactersManager.Instance;
        InstanceCell();
    }
    private void InstanceCell()
    {
        var commonCharacters = charactersManager.GetCharactersByRarity(RarityCharacter.Common);
        for (int i = 0; i < commonCharacters.Length; i++)
        {
            var cell = Instantiate(prefab, commonCharacterCellsConteiner.transform);
            cell.Init(commonCharacters[i]);
            commonCharacterCells.Add(cell);
            cell.CellButton.onClick.AddListener(() => SelectCell(cell));
        }

        var epicCharacters = charactersManager.GetCharactersByRarity(RarityCharacter.Epic);
        for (int i = 0; i < epicCharacters.Length; i++)
        {
            var cell = Instantiate(prefab, epicCharacterCellsConteiner.transform);
            cell.Init(epicCharacters[i]);
            epicCharacterCells.Add(cell);
            cell.CellButton.onClick.AddListener(() => SelectCell(cell));
        }

        var legendaryCharacters = charactersManager.GetCharactersByRarity(RarityCharacter.Legendary);
        for (int i = 0; i < legendaryCharacters.Length; i++)
        {
            var cell = Instantiate(prefab, legendCharacterCellsConteiner.transform);
            cell.Init(legendaryCharacters[i]);
            legendaryCharacterCells.Add(cell);
            cell.CellButton.onClick.AddListener(() => SelectCell(cell));
        }
    }
    public void SelectCell(CharacterUICell characterCell)
    {
        if (selectedCell != null)
        {
            DemonstrationCharacter.reachedFirstRank -= SetActiveSelectButton;
            if (selectedCell == characterCell)
            {
                return;
            }
            else
            {
                selectedCell.RemoveHighlightConteiner();
            }
        }
        selectedCell = characterCell;

        SetActiveSelectButton();
        DemonstrationCharacter.reachedFirstRank += SetActiveSelectButton;

        informationPanel.Init(selectedCell.Character);

        SpawnDemonstrationCharacterPrefab();
    }
    private void SpawnDemonstrationCharacterPrefab()
    {
        if (demoCharacterGameObject != null)
        {
            Destroy(demoCharacterGameObject);
        }
        demoCharacterGameObject = Instantiate(selectedCell.Character.CharacterData.DemonstrationPrefabCharacter, spawnPoint);
        demoCharacterGameObject.transform.localPosition = Vector3.zero;
    }
    private void SetActiveSelectButton()
    {
        selectedCharacterButton.gameObject.SetActive(DemonstrationCharacter.IsOpen);
    }
    public void ConfirmSelection()
    {
        charactersManager.SelectCharacter(DemonstrationCharacter);
        Debug.Log("SelectCharacter");
    }
}

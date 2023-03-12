using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersPanel : MonoBehaviour
{
    private CharactersManager charactersManager;
    private CharacterUICell selectedCell;
    private GameObject demoCharacter;
    [SerializeField] private CharacterInformationPanel informationPanel;
    [SerializeField] private Button selectedCharacterButton;
    [SerializeField] private EquipmentSelectionPanel equipmentSelectionPanel;
    
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private CharacterUICell prefab;

    [SerializeField] private GameObject commonCharacterCellsConteiner;
    [SerializeField] private GameObject epicCharacterCellsConteiner;
    [SerializeField] private GameObject legendCharacterCellsConteiner;

    private List<CharacterUICell> commonCharacterCells = new();
    private List<CharacterUICell> epicCharacterCells = new();
    private List<CharacterUICell> legendaryCharacterCells = new();

    public Character SelectCharacter => selectedCell.Character;

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
            cell.CellButton.onClick.AddListener(() => DemonstrateCharacter(cell));
        }

        var epicCharacters = charactersManager.GetCharactersByRarity(RarityCharacter.Epic);
        for (int i = 0; i < epicCharacters.Length; i++)
        {
            var cell = Instantiate(prefab, epicCharacterCellsConteiner.transform);
            cell.Init(epicCharacters[i]);
            epicCharacterCells.Add(cell);
            cell.CellButton.onClick.AddListener(() => DemonstrateCharacter(cell));
        }

        var legendaryCharacters = charactersManager.GetCharactersByRarity(RarityCharacter.Legendary);
        for (int i = 0; i < legendaryCharacters.Length; i++)
        {
            var cell = Instantiate(prefab, legendCharacterCellsConteiner.transform);
            cell.Init(legendaryCharacters[i]);
            legendaryCharacterCells.Add(cell);
            cell.CellButton.onClick.AddListener(() => DemonstrateCharacter(cell));
        }
    }
    public void DemonstrateCharacter(CharacterUICell characterCell)
    {
        if (selectedCell != null)
        {
            if (selectedCell == characterCell)
            {
                return;
            }
            else
            {
                selectedCell.RemoveHighlightConteiner();
            }
        }
        selectedCharacterButton.interactable = characterCell.Character.IsOpen;

        selectedCell = characterCell;
        informationPanel.Init(selectedCell.Character);

        SpawnDemonstrationCharacterPrefab();
    }
    private void SpawnDemonstrationCharacterPrefab()
    {
        if (demoCharacter != null)
        {
            Destroy(demoCharacter);
        }
        demoCharacter = Instantiate(selectedCell.Character.CharacterData.DemonstrationPrefabCharacter);
        demoCharacter.transform.position = spawnPoint.position;
        demoCharacter.transform.Rotate(0, 180, 0);
        demoCharacter.transform.SetParent(transform);
    }
}

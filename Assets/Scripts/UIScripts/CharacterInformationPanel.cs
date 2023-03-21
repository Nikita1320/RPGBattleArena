using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInformationPanel : MonoBehaviour
{
    [SerializeField] private CharacterImprovementInformationPanel improvementInformationPanel;
    [SerializeField] private CharacterInventoryPanel characterInventoryPanel;
    [SerializeField] private CharacterStatPanel statPanel;
    [SerializeField] private UpgradeCharacterPanel upgradeCharacterPanel;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text nameCHaracterText;
    [SerializeField] private Button abilityImprovementMenuButton;
    private Character character;

    private void Start()
    {
        //abilityImprovementMenuButton.onClick.AddListener(() => )
    }
    public void Init(Character _character)
    {
        if (character == _character)
        {
            return;
        }
        if (character == null)
        {
            nameCHaracterText.gameObject.SetActive(true);
            improvementInformationPanel.gameObject.SetActive(true);
        }
        character = _character;

        nameCHaracterText.text = character.CharacterData.Name;

        improvementInformationPanel.Init(character);
        improvementInformationPanel.gameObject.SetActive(true);

        statPanel.Init(character);
        statPanel.gameObject.SetActive(false);

        characterInventoryPanel.Init(character);

        upgradeCharacterPanel.gameObject.SetActive(false);
    }
    public void OpenUpgradeCharacterPanel()
    {
        upgradeCharacterPanel.Init(character);
    }
    public void Reset()
    {
        improvementInformationPanel.gameObject.SetActive(true);
        statPanel.gameObject.SetActive(false);
        upgradeCharacterPanel.gameObject.SetActive(false);
    }
}

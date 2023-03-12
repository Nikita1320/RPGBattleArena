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
    [SerializeField] private Text nameCHaracterText;
    private Character character;

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

        statPanel.Init(character);

        characterInventoryPanel.Init(character);

        upgradeCharacterPanel.Init(character);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestDescription : MonoBehaviour
{
    [SerializeField] private Text nameChestText;
    [SerializeField] private Text descriptionChestText;
    [SerializeField] private GameObject descriptionContentsRewardEquipments;
    [SerializeField] private GameObject descriptionContentsRewardCard;

    [SerializeField] private GameObject descriptionContentsRewardEquipmentsUnique;
    [SerializeField] private GameObject descriptionContentsRewardCardUnique;

    [SerializeField] private List<EquipmentWithUniqueProbability> equipmentsWithUniqueProbabilitie;
    [SerializeField] private List<CharacterCardWithUniqueProbability> characterCardsWithUniqueProbability;

    [SerializeField] private EquipmentWithUniqueProbability equipmentWithUniqueProbabilityPrefab;
    [SerializeField] private CharacterCardWithUniqueProbability cardWithUniqueProbabilityPrefab;

    [SerializeField] private EquipmentRewardCell equipmentRewardCellPrefab;
    [SerializeField] private CharacterCardRewardCell cardRewardCellPrefab;

    [SerializeField] private GameObject commonEquipmentPanel;
    [SerializeField] private GameObject epicEquipmentPanel;
    [SerializeField] private GameObject legendaryEquipmentPanel;
    [SerializeField] private GameObject commonCharacterCardPanel;
    [SerializeField] private GameObject epicCharacterCardPanel;
    [SerializeField] private GameObject legendaryCharacterCardPanel;

    [SerializeField] private Button commonEquipmentButton;
    [SerializeField] private Button epicEquipmentButton;
    [SerializeField] private Button legendaryEquipmentButton;
    [SerializeField] private Button commonCharacterCardButton;
    [SerializeField] private Button epicCharacterCardButton;
    [SerializeField] private Button legendaryCharacterCardButton;

    [SerializeField] private Text commonEquipmentProbabilityText;
    [SerializeField] private Text epicEquipmentProbabilityText;
    [SerializeField] private Text legendaryEquipmentProbabilityText;
    [SerializeField] private Text commonCharacterCardProbabilityText;
    [SerializeField] private Text epicCharacterCardProbabilityText;
    [SerializeField] private Text legendaryCharacterCardProbabilityText;

    [SerializeField] private GameObject contentChestPanel;
    [SerializeField] private List<GameObject> contentChestList = new();
    private Chest chest;

    private void Start()
    {
        commonEquipmentButton.onClick.AddListener(() => RenderContentsChest(chest.ChestData.RewardsData.CommonEquipmentRewards.ProbabilityRewards));
        epicEquipmentButton.onClick.AddListener(() => RenderContentsChest(chest.ChestData.RewardsData.EpicEquipmentRewards.ProbabilityRewards));
        legendaryEquipmentButton.onClick.AddListener(() => RenderContentsChest(chest.ChestData.RewardsData.LegendaryEquipmentRewards.ProbabilityRewards));

        commonCharacterCardButton.onClick.AddListener(() => RenderContentsChest(chest.ChestData.RewardsData.CommonCharacterCardRewards.ProbabilityRewards));
        epicCharacterCardButton.onClick.AddListener(() => RenderContentsChest(chest.ChestData.RewardsData.EpicCharacterCardRewards.ProbabilityRewards));
        legendaryCharacterCardButton.onClick.AddListener(() => RenderContentsChest(chest.ChestData.RewardsData.LegendaryCharacterCardRewards.ProbabilityRewards));
    }
    public void Init(Chest _chest)
    {
        ClearDescriptionPanel();

        chest = _chest;

        nameChestText.text = chest.ChestData.NameChest;
        descriptionChestText.text = chest.ChestData.DescriptionChest;

        foreach (var i in chest.ChestData.RewardsData.EquipmentRewardsWithCustomProbability)
        {
            foreach (var k in i.ProbabilityRewards)
            {
                var cell = Instantiate(equipmentWithUniqueProbabilityPrefab, descriptionContentsRewardEquipmentsUnique.transform);
                equipmentsWithUniqueProbabilitie.Add(cell);
                cell.Init(k, i.Probability);
            }
        }

        foreach (var i in chest.ChestData.RewardsData.CharacterCardRewardsWithCustomProbability)
        {
            foreach (var k in i.ProbabilityRewards)
            {
                var cell = Instantiate(cardWithUniqueProbabilityPrefab, descriptionContentsRewardCardUnique.transform);
                characterCardsWithUniqueProbability.Add(cell);
                cell.Init(k, i.Probability);
            }
        }

        if (chest.ChestData.RewardsData.CommonEquipmentRewards.ProbabilityRewards.Length > 0)
        {
            commonEquipmentPanel.SetActive(true);
            commonEquipmentProbabilityText.text = (chest.ChestData.RewardsData.CommonEquipmentRewards.Probability * 100).ToString() + "%";
        }
        else
        {
            commonEquipmentPanel.SetActive(false);
        }

        if (chest.ChestData.RewardsData.EpicCharacterCardRewards.ProbabilityRewards.Length > 0)
        {
            epicEquipmentPanel.SetActive(true);
            epicEquipmentProbabilityText.text = (chest.ChestData.RewardsData.EpicEquipmentRewards.Probability * 100).ToString() + "%";
        }
        else
        {
            epicEquipmentPanel.SetActive(false);
        }
            

        if (chest.ChestData.RewardsData.LegendaryEquipmentRewards.ProbabilityRewards.Length > 0)
        {
            legendaryEquipmentPanel.SetActive(true);
            legendaryEquipmentProbabilityText.text = (chest.ChestData.RewardsData.LegendaryEquipmentRewards.Probability * 100).ToString() + "%";
        }
        else
        {
            legendaryEquipmentPanel.SetActive(false);
        }

        if (chest.ChestData.RewardsData.CommonCharacterCardRewards.ProbabilityRewards.Length > 0)
        {
            commonCharacterCardPanel.SetActive(true);
            commonCharacterCardProbabilityText.text = (chest.ChestData.RewardsData.CommonCharacterCardRewards.Probability * 100).ToString() + "%";
        }
        else
        {
            commonCharacterCardPanel.SetActive(false);
        }

        if (chest.ChestData.RewardsData.EpicCharacterCardRewards.ProbabilityRewards.Length > 0)
        {
            epicCharacterCardPanel.SetActive(true);
            epicCharacterCardProbabilityText.text = (chest.ChestData.RewardsData.EpicCharacterCardRewards.Probability * 100).ToString() + "%";
        }
        else
        {
            epicCharacterCardPanel.SetActive(false);
        }

        if (chest.ChestData.RewardsData.LegendaryCharacterCardRewards.ProbabilityRewards.Length > 0)
        {
            legendaryCharacterCardPanel.SetActive(true);
            legendaryCharacterCardProbabilityText.text = (chest.ChestData.RewardsData.LegendaryCharacterCardRewards.Probability * 100).ToString() + "%";
        }
        else
        {
            legendaryCharacterCardPanel.SetActive(false);
        }
    }
    private void ClearDescriptionPanel()
    {
        foreach (var item in equipmentsWithUniqueProbabilitie)
        {
            Destroy(item.gameObject);
        }
        equipmentsWithUniqueProbabilitie.Clear();

        foreach (var item in characterCardsWithUniqueProbability)
        {
            Destroy(item.gameObject);
        }
        characterCardsWithUniqueProbability.Clear();
    }
    private void RenderContentsChest(CharacterCardData[] cardData)
    {
        foreach (var item in cardData)
        {
            var cell = Instantiate(cardRewardCellPrefab, contentChestPanel.transform);
            contentChestList.Add(cell.gameObject);
            cell.Init(item);
        }
    }
    private void RenderContentsChest(EquipmentData[] equipmentData)
    {
        foreach (var item in equipmentData)
        {
            var cell = Instantiate(equipmentRewardCellPrefab, contentChestPanel.transform);
            contentChestList.Add(cell.gameObject);
            cell.Init(item);
        }
    }
    public void ClearContentsPanel()
    {
        foreach (var item in contentChestList)
        {
            Destroy(item);
        }
        contentChestList.Clear();

        foreach (var item in equipmentsWithUniqueProbabilitie)
        {
            Destroy(item);
        }
        contentChestList.Clear();

        foreach (var item in characterCardsWithUniqueProbability)
        {
            Destroy(item);
        }
        contentChestList.Clear();
    }
}

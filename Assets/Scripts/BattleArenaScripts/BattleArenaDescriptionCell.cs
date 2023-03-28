using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleArenaDescriptionCell : MonoBehaviour
{
    [SerializeField] private BattleArenaData battleArenaData;
    [SerializeField] private Button cellButton;
    [SerializeField] private Text countLevelText;

    [SerializeField] private EnemyCell prefabEnemyCell;
    [SerializeField] private GameObject enemysPanel;
    [SerializeField] private List<EnemyCell> enemyCells;

    [SerializeField] private BattleRewardCell prefabBattleRewardCell;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private List<BattleRewardCell> battleRewardCells;
    [SerializeField] private GameObject passedPanel;
    [SerializeField] private GameObject closePanel;
    [SerializeField] private Text closeText;
    private bool levelIsOpen = false;
    private int countLevel;
    public bool isOpen => levelIsOpen;
    public BattleArenaData BattleArenaData => battleArenaData;
    public Button CellButton => cellButton;
    private void Start()
    {
        cellButton = GetComponent<Button>();
    }
    public void Init(BattleArenaData battleArenaData, int countLevel)
    {
        this.battleArenaData = battleArenaData;
        this.countLevel = countLevel;
        countLevelText.text = "Level: " + countLevel;
        closeText.text = $"complete {countLevel - 1} level";
        foreach (var item in battleArenaData.PossibleEnemys)
        {
            InstantiateEnemyCell(item);
        }
        foreach (var item in battleArenaData.BattleRewards)
        {
            InstantiateRewardCell(item);
        }
    }
    private void InstantiateEnemyCell(CharacterData characterData)
    {
        var cell = Instantiate(prefabEnemyCell, enemysPanel.transform);
        cell.Init(characterData);
        enemyCells.Add(cell);
    }
    private void InstantiateRewardCell(BattleReward battleReward)
    {
        var cell = Instantiate(prefabBattleRewardCell, rewardPanel.transform);
        cell.Init(battleReward);
        battleRewardCells.Add(cell);
    }
    public void Close()
    {
        passedPanel.SetActive(false);
        closePanel.SetActive(true);
        levelIsOpen = false;
    }
    public void Open()
    {
        passedPanel.SetActive(false);
        closePanel.SetActive(false);
        levelIsOpen = true;
    }
    public void PassLevel()
    {
        passedPanel.SetActive(true);
        closePanel.SetActive(false);
        levelIsOpen = true;
    }
}

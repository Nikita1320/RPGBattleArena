using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleArenaDescriptionCell : MonoBehaviour
{
    [SerializeField] private BattleArenaData battleArenaData;
    [SerializeField] private Button cellButton;

    [SerializeField] private EnemyCell prefabEnemyCell;
    [SerializeField] private GameObject enemysPanel;
    [SerializeField] private List<EnemyCell> enemyCells;

    [SerializeField] private BattleRewardCell prefabBattleRewardCell;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private List<BattleRewardCell> battleRewardCells;
    public BattleArenaData BattleArenaData => battleArenaData;
    public Button CellButton => cellButton;
    private void Start()
    {
        cellButton = GetComponent<Button>();
    }
    public void Init(BattleArenaData battleArenaData)
    {
        this.battleArenaData = battleArenaData;

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
}

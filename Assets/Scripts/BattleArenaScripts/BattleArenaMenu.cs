using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum TypePassingLevel
{
    Primary,
    Secondary
}
public class BattleArenaMenu : MonoBehaviour
{
    [SerializeField] private GameObject battleArenaDescriptionCellConteiner;
    [SerializeField] private BattleArenaData[] arenaDatas;
    [SerializeField] private BattleArenaDescriptionCell prefab;
    [SerializeField] private List<BattleArenaDescriptionCell> cells;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private BattleArenaDescriptionCell selectedCell;
    [Header("SettingsScrollToCell")]
    [SerializeField] private float speedScroll = 5;
    [SerializeField] private float minDistanceToMove;

    [SerializeField] private Button startButton;
    [SerializeField] private int battleArenaScene;

    private Coroutine scrollCoroutine;
    private static BattleArenaData selectedBattleArenaData;
    private static TypePassingLevel typeCurrentPassingLevel;
    private static int levelPassed = 0;
    public static TypePassingLevel TypeCurrentPassingLevel => typeCurrentPassingLevel;
    public static BattleArenaData SelectedBattleArenaData => selectedBattleArenaData;
    private void Start()
    {
        for (int i = 0; i < arenaDatas.Length; i++)
        {
            var cell = Instantiate(prefab, battleArenaDescriptionCellConteiner.transform);
            if (i < levelPassed)
            {
                cell.Init(arenaDatas[i], i, StateBattleArenaLevel.Passed);
            }
            else if (i == levelPassed)
            {
                cell.Init(arenaDatas[i], i, StateBattleArenaLevel.Current);
            }
            else
            {
                cell.Init(arenaDatas[i], i, StateBattleArenaLevel.Close);
            }
            cells.Add(cell);
            cell.CellButton.onClick.AddListener(() => SelectCell(cell));
        }
        SelectCell(cells[levelPassed]);
        startButton.onClick.AddListener(StartBattleArena);
        Reset();
    }
    public void SelectCell(BattleArenaDescriptionCell descriptionCell)
    {
        if (descriptionCell == selectedCell)
        {
            return;
        }
        startButton.interactable = descriptionCell.isOpen;

        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }
        selectedCell = descriptionCell;
        selectedBattleArenaData = selectedCell.BattleArenaData;
        if (selectedCell.State == StateBattleArenaLevel.Passed)
            typeCurrentPassingLevel = TypePassingLevel.Secondary;
        else
            typeCurrentPassingLevel = TypePassingLevel.Primary;

        var positionInHierarchy = selectedCell.transform.GetSiblingIndex();

        float range = -100 - 230 * positionInHierarchy;

        scrollCoroutine = StartCoroutine(ScrollToCell(range));
    }
    private IEnumerator ScrollToCell(float neededPosition)
    {
        scrollRect.enabled = false;
        var startDifferencePosition = Mathf.Abs(content.anchoredPosition.x - neededPosition);
        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (Mathf.Abs(content.anchoredPosition.x - neededPosition) < minDistanceToMove)
            {
                content.anchoredPosition = new Vector2(neededPosition, content.anchoredPosition.y);
                break;
            }
            Debug.Log(Mathf.Abs(content.anchoredPosition.x - neededPosition));
            Debug.Log(Mathf.Abs(content.anchoredPosition.x - neededPosition) < minDistanceToMove);
            if (content.anchoredPosition.x < neededPosition)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x + speedScroll * Mathf.Abs(content.anchoredPosition.x - neededPosition) / startDifferencePosition, content.anchoredPosition.y);
            }
            else
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x - speedScroll * Mathf.Abs(content.anchoredPosition.x - neededPosition) / startDifferencePosition, content.anchoredPosition.y);
            }
        }
        scrollRect.enabled = true;
    }
    public static void PassLevel()
    {
        levelPassed++;
    }
    public void StopScroll()
    {
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }
    }
    private void StartBattleArena()
    {
        if (CharactersManager.Instance.SelectedCharacter != null)
        {
            SceneLoader.Instance.LoadScene(battleArenaScene);
        }
    }
    private void OnDisable()
    {
        Reset();
    }
    private void Reset()
    {
        var positionInHierarchy = cells[levelPassed].transform.GetSiblingIndex();

        float range = -100 - 230 * positionInHierarchy;

        content.anchoredPosition = new Vector2(range, 0);

        selectedCell = cells[levelPassed];
        selectedBattleArenaData = selectedCell.BattleArenaData;
        startButton.interactable = true;
    }
}

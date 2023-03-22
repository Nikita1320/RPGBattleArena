using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Coroutine scrollCoroutine;
    private static BattleArenaData selectedBattleArenaData;

    public static BattleArenaData SelectedBattleArenaData => selectedBattleArenaData;
    private void Start()
    {
        foreach (var item in arenaDatas)
        {
            var cell = Instantiate(prefab, battleArenaDescriptionCellConteiner.transform);
            cell.Init(item);
            cells.Add(cell);
            cell.CellButton.onClick.AddListener(() => SelectCell(cell));
        }
    }
    public void SelectCell(BattleArenaDescriptionCell descriptionCell)
    {
        if (selectedCell != null)
        {
            DeselectCell();
        }
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }
        selectedCell = descriptionCell;
        selectedBattleArenaData = selectedCell.BattleArenaData;

        var positionInHierarchy = selectedCell.transform.GetSiblingIndex();

        float range = -100 - 230 * positionInHierarchy;
        Debug.Log("!!!");

        scrollCoroutine = StartCoroutine(ScrollToCell(range));
    }
    private IEnumerator ScrollToCell(float neededPosition)
    {
        var startDifferencePosition = Mathf.Abs(content.anchoredPosition.x - neededPosition);
        while (true)
        {
            yield return new WaitForFixedUpdate();
            
            if (content.anchoredPosition.x < neededPosition)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x + speedScroll * (Mathf.Abs(content.anchoredPosition.x - neededPosition) / startDifferencePosition), content.anchoredPosition.y);
            }
            else
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x - speedScroll * (Mathf.Abs(content.anchoredPosition.x - neededPosition) / startDifferencePosition), content.anchoredPosition.y);
            }
            Debug.Log(Mathf.Abs(content.anchoredPosition.x - neededPosition));
            if (Mathf.Abs(content.anchoredPosition.x - neededPosition) < minDistanceToMove)
            {
                content.anchoredPosition = new Vector2(neededPosition, content.anchoredPosition.y);
                break;
            }
        }
    }
    public void DeselectCell()
    {

    }
}

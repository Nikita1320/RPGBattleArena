using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleArenaMenu : MonoBehaviour
{
    [SerializeField] private BattleArenaData[] arenaDatas;
    [SerializeField] private BattleArenaDescriptionCell prefab;
    [SerializeField] private BattleArenaDescriptionCell[] cells;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private BattleArenaDescriptionCell selectedCell;
    [SerializeField] private float speedScroll = 5;
    private Coroutine scrollCoroutine;
    private static BattleArenaData selectedBattleArenaData;

    public static BattleArenaData SelectedBattleArenaData => selectedBattleArenaData;
    private void Start()
    {
        foreach (var item in cells)
        {
            item.CellButton.onClick.AddListener(() => SelectCell(item));
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

        var positionInHierarchy = selectedCell.transform.GetSiblingIndex();

        float range = -100 - 230 * positionInHierarchy;
        Debug.Log("!!!");

        scrollCoroutine = StartCoroutine(ScrollToCell(range));
    }
    private IEnumerator ScrollToCell(float neededPosition)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            
            if (content.anchoredPosition.x < neededPosition)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x + speedScroll, content.anchoredPosition.y);
            }
            else
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x - speedScroll, content.anchoredPosition.y);
            }
            Debug.Log(Mathf.Abs(content.anchoredPosition.x - neededPosition));
            if (Mathf.Abs(content.anchoredPosition.x - neededPosition) < 20)
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

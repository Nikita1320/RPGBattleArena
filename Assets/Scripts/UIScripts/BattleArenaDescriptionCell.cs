using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleArenaDescriptionCell : MonoBehaviour
{
    [SerializeField] private Button cellButton;
    public Button CellButton => cellButton;
    private void Start()
    {
        cellButton = GetComponent<Button>();
    }

}

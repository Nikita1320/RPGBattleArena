using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected Image rareImage;
    [SerializeField] protected Button button;
    public Button CellButton => button;
    public Image ItemImage => itemImage;
    public Image RareImage => rareImage;
    private void Start()
    {
        button = GetComponent<Button>();
    }
}

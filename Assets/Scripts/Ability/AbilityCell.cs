using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCell : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private Image lockImage;
    [SerializeField] private Text lockText;
    [SerializeField] private Button cellButton;
    [SerializeField] private Image notActiveImage;
    private bool isLock = true;
    [SerializeField] private Ability ability;
    public bool IsLock => isLock;
    public Button CellButton => cellButton;
    public Ability Ability => ability;
    public Text LockText => lockText;
    public void Init(Ability _ability)
    {
        ability = _ability;
        abilityImage.sprite = ability.Icon;
    }
    public void LockCell()
    {
        lockImage.gameObject.SetActive(true);
        isLock = true;
    }
    public void UnLockCell()
    {
        lockImage.gameObject.SetActive(false);
        isLock = false;
    }
    public void RemoveNotActiveImage()
    {
        notActiveImage.gameObject.SetActive(false);
    }
    public void SetNotActiveImage()
    {
        notActiveImage.gameObject.SetActive(true);
    }
}

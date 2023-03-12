using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    private void Start()
    {
        health.changedHP += RenderHP;
        text.text = health.HP.ToString();
    }
    private void RenderHP()
    {
        text.text = health.HP.ToString();
        image.fillAmount = health.MaxHP / health.HP;
    }
}

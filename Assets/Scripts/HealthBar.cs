using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;

    public void Init(Health health, Color color)
    {
        if (health != null)
        {
            health.changedHP += ShowHealth;
        }
        fillImage.color = color;
    }
    private void ShowHealth()
    {
        fillImage.fillAmount = health.HP / health.MaxHP;
    }
}

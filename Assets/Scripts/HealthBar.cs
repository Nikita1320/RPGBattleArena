using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        if (health != null)
        {
            health.changedHP += ShowHealth;
        }
    }
    private void ShowHealth()
    {
        fillImage.fillAmount = health.HP / health.MaxHP;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void TakedDamage();
    public delegate void ChangedHP();
    public delegate void Died();

    public TakedDamage takedDamage;
    public ChangedHP changedHP;
    public Died diedEvent;

    [SerializeField] private float healthPoint;
    [SerializeField] private float maxHealthPoint;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Color colorTeem;
    public float MaxHP => maxHealthPoint;
    public float HP => healthPoint;

    private void Awake()
    {
        healthPoint = maxHealthPoint;
    }
    public void TakeDamage(float incomingDamage)
    {
        healthPoint -= incomingDamage;
        changedHP?.Invoke();
        if (healthPoint <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        diedEvent?.Invoke();
        Destroy(gameObject);
    }
    public virtual void InitializeStat(Character character, Color color)
    {
        colorTeem = color;
        healthBar.Init(this, color);
    }
}

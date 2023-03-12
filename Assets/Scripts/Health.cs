using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void TakedDamage();
    public delegate void ChangedHP();

    public TakedDamage takedDamage;
    public ChangedHP changedHP;

    [SerializeField] private float healthPoint;
    [SerializeField] private float maxHealthPoint;

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
        Destroy(gameObject);
    }
}

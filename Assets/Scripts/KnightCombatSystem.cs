using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCombatSystem : CombatSystem
{
    [SerializeField] private FloatStat radiusAttack;
    [SerializeField] private FloatStat rangeAttack;
    private Collider[] damagableObject;

    public FloatStat RangeAttack => rangeAttack;
    public FloatStat RadiusAttack => radiusAttack;
    public override bool Attack()
    {
        Debug.Log("TryAttack");
        if (currentTimeToReadyattack == 0)
        {
            isAttacking = true;
            currentTimeToReadyattack = attackSpeed.Value;
            mayAttack = false;
            animator.SetTrigger("Attack");
            return true;
        }
        else
        {
            return false;
        }
    }
    public void GetDamage()
    {
        Debug.Log("GetDamage");
        Debug.Log(transform.position + transform.forward);
        damagableObject = Physics.OverlapSphere(transform.position + transform.forward, radiusAttack.Value, damagableLayer);
        for (int i = 0; i < damagableObject.Length; i++)
        {
            Debug.Log(damagableObject[i].name);
        }
        var allies = new List<Health>(Allies).ConvertAll(x => x.gameObject);

        for (int i = 0; i < damagableObject.Length; i++)
        {
            if (!allies.Contains(damagableObject[i].gameObject))
            {
                damagableObject[i].gameObject.GetComponent<Health>().TakeDamage(damage.Value);
                Debug.Log($"GetDamageTo{damagableObject[i].gameObject}");
            }
        }
        isAttacking = false;
        attacked?.Invoke();
    }
}

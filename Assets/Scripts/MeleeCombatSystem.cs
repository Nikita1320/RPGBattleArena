using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombatSystem : CombatSystem, IMeleeAttack
{
    [SerializeField] private FloatStat radiusAttack;
    [SerializeField] private FloatStat rangeAttack;
    private Collider[] damagableObject;
    public FloatStat RangeAttack => rangeAttack;
    public FloatStat RadiusAttack => radiusAttack;
    public override bool Attack()
    {
        Debug.Log("TryAttack");
        if (currentTimeToReadyAttack == 0)
        {
            isAttacking = true;
            currentTimeToReadyAttack = attackSpeed.GetCurveValue();
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
            }
        }
        isAttacking = false;
        attacked?.Invoke();
    }
    public override void InitializeStat(Character character)
    {
        base.InitializeStat(character);
        radiusAttack = new FloatStat(character.Stats[TypeStat.RangeArea].Value);
        rangeAttack = new FloatStat(character.Stats[TypeStat.RangeAttack].Value);
    }
}

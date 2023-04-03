using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatSystem : MonoBehaviour
{
    public delegate void Attacked();
    public Attacked attacked;

    [SerializeField] protected LayerMask damagableLayer = 9;
    [SerializeField] protected List<Health> allies;
    [SerializeField] protected CurveStat attackSpeed;
    [SerializeField] protected FloatStat damage;
    [SerializeField] private AnimationCurve attackSpeedCurve;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float currentTimeToReadyattack = 0;
    private float maxStatValue = 1000;
    private float maxCurveValue = 0.3f;
    protected bool mayAttack = true;
    protected bool isAttacking = false;
    public bool MayAttack => mayAttack;
    public bool IsAttacking => isAttacking;
    public FloatStat Damage => damage;
    public FloatStat AttackSpeed => attackSpeed;
    public Health[] Allies => allies.ToArray();
    public LayerMask DamagableLayer => damagableLayer;
    private void Start()
    {
        allies.Add(GetComponent<Health>());
    }
    public void Update()
    {
        if (currentTimeToReadyattack != 0)
        {
            currentTimeToReadyattack -= Time.deltaTime;
            if (currentTimeToReadyattack <= 0)
            {
                currentTimeToReadyattack = 0;
                mayAttack = true;
            }
        }
    }
    public virtual void InitializeStat(Character character)
    {
        damage = new FloatStat(character.Stats[TypeStat.Damage].Value);
        attackSpeed = new CurveStat(character.Stats[TypeStat.AttackSpeed].Value, attackSpeedCurve, maxStatValue, maxCurveValue);
    }
    public void AddAllies(Health ally)
    {
        allies.Add(ally);
    }
    public abstract bool Attack();
}

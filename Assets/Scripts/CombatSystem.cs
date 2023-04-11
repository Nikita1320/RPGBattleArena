using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeAttack
{
    public FloatStat RangeAttack { get; }
    public FloatStat RadiusAttack { get; }
}
public interface IRangeAttack
{
    public FloatStat SpeedBullet { get; }
    public FloatStat LifeTimeBullet { get; }
}
public abstract class CombatSystem : MonoBehaviour
{
    public delegate void Attacked();
    public Attacked attacked;

    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask damagableLayer = 9;
    [SerializeField] protected List<Health> allies;
    [SerializeField] protected CurveStat attackSpeed;
    [SerializeField] protected FloatStat damage;
    [SerializeField] protected float currentTimeToReadyAttack = 0;

    [Header("SettingsAttackSpeedCurve")]
    [SerializeField] internal AnimationCurveData attackSpeedCurve;

    protected bool mayAttack = true;
    protected bool isAttacking = false;
    public bool MayAttack => mayAttack;
    public bool IsAttacking => isAttacking;
    public FloatStat Damage => damage;
    public CurveStat AttackSpeed => attackSpeed;
    public Health[] Allies => allies.ToArray();
    public LayerMask DamagableLayer => damagableLayer;
    public float CurrentTimeToReadyAttack => currentTimeToReadyAttack;
    private void Start()
    {
        allies.Add(GetComponent<Health>());
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        if (currentTimeToReadyAttack != 0)
        {
            currentTimeToReadyAttack -= Time.deltaTime;
            if (currentTimeToReadyAttack <= 0)
            {
                currentTimeToReadyAttack = 0;
                mayAttack = true;
            }
        }
    }
    public virtual void InitializeStat(Character character)
    {
        damage = new FloatStat(character.Stats[TypeStat.Damage].Value);
        attackSpeed = new CurveStat(character.Stats[TypeStat.AttackSpeed].Value, attackSpeedCurve.GetCurve);
    }
    public void AddAllies(Health ally)
    {
        allies.Add(ally);
    }
    public abstract bool Attack();
}

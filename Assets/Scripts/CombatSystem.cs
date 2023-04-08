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
    [SerializeField] protected float currentTimeToReadyattack = 0;

    [Header("SettingsAttackSpeedCurve")]
    [SerializeField] internal AnimationCurveData attackSpeedCurve;

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
        animator = GetComponent<Animator>();
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
        attackSpeed = new CurveStat(character.Stats[TypeStat.AttackSpeed].Value, attackSpeedCurve.GetCurve);
    }
    public void AddAllies(Health ally)
    {
        allies.Add(ally);
    }
    public abstract bool Attack();
}

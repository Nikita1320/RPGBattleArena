using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCombatSystem : CombatSystem, IRangeAttack
{
    [SerializeField] private int level = 1;

    [SerializeField] private Bullet prefBullet;
    [SerializeField] private Transform pointSpawnBullet;
    private Bullet currentBullet;
    private float offsetBullet = 0.6f;
    private float startOffset;

    [SerializeField] private FloatStat speedBullet;
    [SerializeField] private FloatStat lifeTimeBullet;
    public FloatStat SpeedBullet => speedBullet;
    public FloatStat LifeTimeBullet => lifeTimeBullet;
    private void Start()
    {
        
    }
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
    public void MakeShot()
    {
        Debug.Log("Shot");
        for (int i = 0; i < level; i++)
        {
            currentBullet = Instantiate(prefBullet, pointSpawnBullet);
            currentBullet.transform.localPosition += new Vector3(startOffset + offsetBullet * i, 0, 0);
            currentBullet.gameObject.transform.parent = null;
            currentBullet.Init(this);
        }
        Debug.Log("EndInstanceBullet");
        isAttacking = false;
        attacked?.Invoke();
    }
    public void LevelUp()
    {
        level++;
        startOffset = -((offsetBullet * (level - 1)) / 2);
    }
}

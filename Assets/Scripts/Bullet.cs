using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public delegate void CollisionWithWall();
    public CollisionWithWall collisionWithWall;

    public delegate void CollisionWithEnemy();
    public CollisionWithEnemy collisionWithEnemy;

    private RangeCombatSystem magicianCombatSystem;
    private bool isInit = false;
    private float lifeTime = 0;
    private List<Health> allies;

    private void Update()
    {
        if (isInit)
        {
            transform.position += transform.forward * magicianCombatSystem.SpeedBullet.Value * Time.deltaTime;
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init(RangeCombatSystem magicianCombatSystem)
    {
        this.magicianCombatSystem = magicianCombatSystem;
        lifeTime = magicianCombatSystem.LifeTimeBullet.Value;
        allies = new List<Health>(magicianCombatSystem.Allies);
        isInit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.ToString() == "Wall")
        {

        }
        if (other.gameObject.TryGetComponent(out Health health))
        {
            if (allies.Contains(health) == false)
            {
                health.TakeDamage(magicianCombatSystem.Damage.Value);
                Destroy(gameObject);
            }
        }
    }
}

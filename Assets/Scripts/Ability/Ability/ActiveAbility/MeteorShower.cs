using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : CooldownableActiveAbility
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private float[] damageRadius;
    [SerializeField] private float[] damage;
    [SerializeField] private float[] duration;
    [SerializeField] private LayerMask damagableLayer;
    private ParticleSystem particle;

    public override void Init(int level, GameObject character)
    {
        base.Init(level, character);
    }
    public override bool Use()
    {
        if (currentRemainingTimeCooldown != 0)
        {
            return false;
        }
        particle = Instantiate(particlePrefab, transform);
        particle.transform.localPosition = Vector3.zero;
        particle.Play();
        StartCoroutine(GetDamageFromMeteor());
        StartCooldown();
        return true;
    }
    private IEnumerator GetDamageFromMeteor()
    {
        float time = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            time += 0.5f;
            Collider[] damagable = Physics.OverlapSphere(gameObject.transform.position, damageRadius[level], damagableLayer);
            var allies = TeemsManager.Instance.GetAllies(gameObject);
            foreach (var item in damagable)
            {
                if (item.TryGetComponent(out Health health) && !allies.Contains(item.gameObject))
                {
                    health.TakeDamage(damage[level]);
                }
            }
            if (time >= duration[level])
            {
                Destroy(particle.gameObject);
                break;
            }
        }
    }
}

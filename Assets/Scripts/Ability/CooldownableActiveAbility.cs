using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CooldownableActiveAbility : ActiveAbility, ICooldownable
{
    [SerializeField] protected float[] cooldown;
    [SerializeField] protected float currentRemainingTimeCooldown;

    private Coroutine cooldownCororutine;

    public float[] CooldownTime => cooldown;
    public float CurrentCooldown => cooldown[level];
    public float CurrentRemainingTimeCooldown => currentRemainingTimeCooldown;
    public Action CooldownStarted { get; set; }
    public Action CooldownEnded { get; set; }

    public void EndCooldown()
    {
        mayUse = true;
        StopCoroutine(cooldownCororutine);
        currentRemainingTimeCooldown = 0;
        CooldownEnded?.Invoke();
    }
    protected IEnumerator CooldownCororutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentRemainingTimeCooldown -= Time.deltaTime;
            if (currentRemainingTimeCooldown <= 0)
            {
                EndCooldown();
            }
        }
    }
    public void StartCooldown()
    {
        mayUse = false;
        currentRemainingTimeCooldown = cooldown[level];
        CooldownStarted?.Invoke();
        if (cooldownCororutine != null)
        {
            StopCoroutine(cooldownCororutine);
        }
        cooldownCororutine = StartCoroutine(CooldownCororutine());
    }
}

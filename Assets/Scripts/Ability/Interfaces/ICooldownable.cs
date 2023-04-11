using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICooldownable
{
    public Action CooldownStarted { get; set; }
    public Action CooldownEnded { get; set; }
    public float[] CooldownTime { get; }
    public float CurrentCooldown { get; }
    public float CurrentRemainingTimeCooldown { get; }
    public void StartCooldown();
    public void EndCooldown();
}

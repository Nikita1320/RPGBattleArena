using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehavior : CharacterBehaviour
{
    [SerializeField] private float angularAttack;
    [SerializeField] private Collider[] damagableObject;
    [SerializeField] private float timePursuit;
    private IMeleeAttack meleeAttack;
    public IMeleeAttack MeleeAttack => meleeAttack;
    public float TimePursuit => timePursuit;
    private void Start()
    {
        combatSystem.attacked += Strafe;
        meleeAttack = GetComponent<IMeleeAttack>();
        InitState();
        SetDefaultState();
    }
    public override void InitState()
    {
        states.Add(StateBehaviour.ConductingBattle, new MeleeConductingBattleState(this));
        states.Add(StateBehaviour.Idle, new IdleState(this));
    }
}

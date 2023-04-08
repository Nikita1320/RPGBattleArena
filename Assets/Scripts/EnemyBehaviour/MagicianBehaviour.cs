using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagicianBehaviour : CharacterBehaviour
{
    [SerializeField] private float angularAttack;
    [SerializeField] private Collider[] damagableObject;
    [SerializeField] private GameObject target;
    [SerializeField] private float responseDistance;
    private IRangeAttack rangeAttack;
    public IRangeAttack RangeAttack => rangeAttack;
    private void Start()
    {
        combatSystem.attacked += Strafe;
        rangeAttack = GetComponent<IRangeAttack>();
    }
    private float RandomBetween2Value(float value1, float value2)
    {
        float[] values = { value1, value2 };

        return values[Random.Range(0, 1)];
    }
    public override void InitState()
    {
        //states.Add(StateBehaviour.ConductingBattle, new MeleeConductingBattleState(this));
        states.Add(StateBehaviour.Idle, new IdleState(this));
    }
}

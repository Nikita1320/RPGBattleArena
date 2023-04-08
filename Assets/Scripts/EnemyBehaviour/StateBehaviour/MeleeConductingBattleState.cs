using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeConductingBattleState : IStateBehaviour
{
    private KnightBehavior knightBehavior;
    private float currentTimePursuit;
    private float DistanceToTarget => Vector3.Distance(knightBehavior.CurrentTargetEnemy.transform.position, knightBehavior.gameObject.transform.position);
    private bool IsContinuePersecution => knightBehavior.CurrentTargetEnemy != null &&
            DistanceToTarget < knightBehavior.RadiusEndPursuit &&
            currentTimePursuit >= 0;
    public MeleeConductingBattleState(KnightBehavior knightBehavior)
    {
        this.knightBehavior = knightBehavior;
    }
    public void Enter()
    {
        Debug.Log($"Target = {knightBehavior.CurrentTargetEnemy}");
        knightBehavior.MovementController.Move(knightBehavior.CurrentTargetEnemy.transform.position);
        currentTimePursuit = knightBehavior.TimePursuit;
    }

    public void Exit()
    {
        knightBehavior.ResetTarget();
    }

    public void Update()
    {
        currentTimePursuit -= Time.deltaTime;
        if (IsContinuePersecution)
        {
            if (DistanceToTarget < knightBehavior.MeleeAttack.RangeAttack.Value)
            {
                if (knightBehavior.CombatSystem.MayAttack)
                {
                    knightBehavior.CombatSystem.Attack();
                    currentTimePursuit = knightBehavior.TimePursuit;
                }
                else
                {
                    if (knightBehavior.MovementController.IsMoving == false)
                    {
                        knightBehavior.Strafe();
                    }
                }
            }
            else
            {
                knightBehavior.MovementController.Move(knightBehavior.CurrentTargetEnemy.transform.position);
            }
        }
        else
        {
            knightBehavior.ChangeState(StateBehaviour.Idle);
        }
    }
}

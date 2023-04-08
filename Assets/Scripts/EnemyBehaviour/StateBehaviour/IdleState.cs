using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IdleState : IStateBehaviour
{
    private CharacterBehaviour characterBehaviour;
    private Vector3 currentTargetPoint;
    private Coroutine coroutine;
    private float travelTimeToPurpose = 5;
    private float remainingTime = 0;
    public IdleState(CharacterBehaviour characterBehaviour)
    {
        this.characterBehaviour = characterBehaviour;
        characterBehaviour.FindTarget();
    }
    public void Enter()
    {
        SetTargetPosition();
        coroutine = characterBehaviour.StartCoroutine(characterBehaviour.FindTargetCoroutine());
    }

    public void Exit()
    {
        remainingTime = 0;
        if (coroutine != null)
        {
            characterBehaviour.StopCoroutine(coroutine);
        }
    }

    public void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0 || characterBehaviour.MovementController.IsMoving == false)
        {
            SetTargetPosition();
        }
        if (characterBehaviour.CurrentTargetEnemy != null)
        {
            Debug.Log($"IFoundTarget = {characterBehaviour.CurrentTargetEnemy}");
            characterBehaviour.ChangeState(StateBehaviour.ConductingBattle);
        }
    }
    private void SetTargetPosition()
    {
        characterBehaviour.RandomPoint(out currentTargetPoint);
        characterBehaviour.MovementController.Move(currentTargetPoint);
        remainingTime = travelTimeToPurpose;
    }
}

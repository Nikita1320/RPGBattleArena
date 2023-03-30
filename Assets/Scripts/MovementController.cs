using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private FloatStat moveSpeed;
    [SerializeField] private FloatStat rotateSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private Coroutine rotateToCoroutine;
    [SerializeField] private bool iHavePath;
    public bool isMoving => navMeshAgent.hasPath;

    private void Update()
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
        if (!iHavePath)
        {
            navMeshAgent.ResetPath();
        }
        iHavePath = navMeshAgent.hasPath;
    }
    public void Move(Vector3 direction)
    {
        navMeshAgent.SetDestination(direction);
        Debug.Log(direction);

        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed.Value * Time.deltaTime, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction - transform.position), rotateSpeed);
    }
    private IEnumerator RotateTo(Vector3 direction)
    {
        while (true)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed.Value * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    public virtual void InitializeStat(Character character)
    {
        moveSpeed = new FloatStat(character.Stats[TypeStat.MoveSpeed].Value);
        rotateSpeed = new FloatStat(character.Stats[TypeStat.RotateSpeed].Value);
        navMeshAgent.speed = moveSpeed.Value;
        navMeshAgent.angularSpeed = rotateSpeed.Value;
        //navMeshAgent.updateRotation = false;
    }
}

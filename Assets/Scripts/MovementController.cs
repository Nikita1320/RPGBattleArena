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
    [SerializeField] private bool iHavePath;
    public bool isMoving => navMeshAgent.hasPath;
    private void Start()
    {
        navMeshAgent.speed = moveSpeed.Value;
        navMeshAgent.angularSpeed = rotateSpeed.Value;
    }
    private void Update()
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
        iHavePath = navMeshAgent.hasPath;
    }
    public void Move(Vector3 direction)
    {
        navMeshAgent.SetDestination(direction);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction - transform.position), rotateSpeed);
    }
    public virtual void InitializeStat(Character character)
    {

    }
}

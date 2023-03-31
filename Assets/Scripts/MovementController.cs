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
        if (navMeshAgent.remainingDistance <= 0.1)
        {
            navMeshAgent.ResetPath();
        }
        if (navMeshAgent.hasPath)
        {
            var direction = navMeshAgent.steeringTarget - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed.Value * Time.deltaTime);



            /*Vector3 newDirection = Vector3.RotateTowards(transform.forward, navMeshAgent.steeringTarget, rotateSpeed.Value * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);*/
        }
        /*if (!iHavePath)
        {
            navMeshAgent.ResetPath();
        }*/
        iHavePath = navMeshAgent.hasPath;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Move(Vector3.zero);
        }
    }
    public void Move(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
        /*if (rotateToCoroutine != null)
        {
            StopCoroutine(rotateToCoroutine);
        }
        rotateToCoroutine = StartCoroutine(RotateTo());*/
        Debug.Log(targetPosition);

        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed.Value * Time.deltaTime, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction - transform.position), rotateSpeed);
    }
    private IEnumerator RotateTo()
    {
        while (true)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, navMeshAgent.steeringTarget, rotateSpeed.Value * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    public virtual void InitializeStat(Character character)
    {
        moveSpeed = new FloatStat(character.Stats[TypeStat.MoveSpeed].Value);
        rotateSpeed = new FloatStat(character.Stats[TypeStat.RotateSpeed].Value);
        navMeshAgent.speed = moveSpeed.Value;
        navMeshAgent.angularSpeed = rotateSpeed.Value;
        navMeshAgent.updateRotation = false;
    }
}

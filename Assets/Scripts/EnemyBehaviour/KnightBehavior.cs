using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehavior : CharacterBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private KnightCombatSystem combatSystem;
    [SerializeField] private float angularAttack;
    [SerializeField] private Collider[] damagableObject;
    [SerializeField] private GameObject target;
    private float counter = 1;
    private bool inFinding = false;

    private void Start()
    {
        combatSystem.attacked += Strafe;
    }
    private void Update()
    {
        if (target == null)
        {
            if (inFinding == false)
            {
                StartCoroutine(FindTargetCoroutine());
            }
        }
        else
        {
            var distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

            if (distanceToTarget < 25)
            {
                if (distanceToTarget <= combatSystem.RangeAttack.Value)
                {
                    var angleBetweenTarget = Mathf.Abs(Vector3.Angle(transform.forward, target.transform.position - transform.position));

                    if (angleBetweenTarget <= angularAttack)
                    {
                        if (combatSystem.MayAttack)
                        {
                            movementController.Move(transform.position);
                            combatSystem.Attack();
                            //Strafe();
                        }
                    }
                    else
                    {
                        if (combatSystem.MayAttack)
                        {
                            counter -= Time.deltaTime;
                            if (counter <= 0)
                            {
                                movementController.Move(transform.position);
                                combatSystem.Attack();
                                //Strafe();
                                counter = 1;
                            }
                            else
                            {
                                movementController.Move(target.transform.position);
                            }
                        }
                        else
                        {
                            if (!movementController.isMoving && !combatSystem.IsAttacking)
                            {
                                Strafe();
                            }
                        }
                    }
                }
                else
                {
                    movementController.Move(target.transform.position);
                }
            }
            else
            {
                movementController.Move(target.transform.position);
            }
        }
    }
    private void Strafe()
    {
        var direction = (transform.position - target.transform.position).normalized;

        direction.z += Random.Range(0, 2);
        direction.x += Random.Range(-3, 4);
        Debug.Log("strafe");

        movementController.Move(transform.position + direction);

    }
    private void FindTarget()
    {
        damagableObject = Physics.OverlapSphere(transform.position, 30, combatSystem.DamagableLayer);
        var allies = new List<Health>(combatSystem.Allies).ConvertAll(x => x.gameObject);

        for (int i = 0; i < damagableObject.Length; i++)
        {
            if (!allies.Contains(damagableObject[i].gameObject))
            {
                target = damagableObject[i].gameObject;
                return;
            }
        }
        target = null;
    }
    private IEnumerator FindTargetCoroutine()
    {
        inFinding = true;
        yield return null;

        FindTarget();
        inFinding = false;
    }
}

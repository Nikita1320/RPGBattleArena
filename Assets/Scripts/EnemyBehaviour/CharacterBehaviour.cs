using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateBehaviour
{
    Idle,
    ConductingBattle
}
public abstract class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] protected MovementController movementController;
    [SerializeField] protected CombatSystem combatSystem;
    [SerializeField] protected GameObject currentTargetEnemy;
    [SerializeField] protected float searchRadiusPoint = 5;
    [SerializeField] protected float searchRadiusEnemy = 10;
    [SerializeField] protected float radiusEndPursuit = 15;
    [SerializeField] protected StateBehaviour currentStateCharacter;
    protected Dictionary<StateBehaviour,IStateBehaviour> states = new();
    public float SearchRadiusPoint => searchRadiusPoint;
    public float SearchRadiusEnemy => searchRadiusEnemy;
    public float RadiusEndPursuit => radiusEndPursuit;
    public StateBehaviour CurrentStateCharacter => currentStateCharacter;
    public GameObject CurrentTargetEnemy => currentTargetEnemy;
    public virtual MovementController MovementController => movementController;
    public virtual CombatSystem CombatSystem => combatSystem;
    protected virtual void Update()
    {
        states[currentStateCharacter].Update();
    }
    public IEnumerator FindTargetCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (FindTarget())
            {
                Debug.Log($"EndFindCoroutine target = {currentTargetEnemy}");
                break;
            }
        }
    }
    public virtual void Strafe()
    {
        var direction = (transform.position - currentTargetEnemy.transform.position).normalized;

        direction.z += Random.Range(0, 2);
        direction.x += Random.Range(-3, 4);
        Debug.Log("strafe");

        movementController.Move(transform.position + direction);
    }
    public bool FindTarget()
    {
        var damagableObject = Physics.OverlapSphere(transform.position, searchRadiusEnemy, combatSystem.DamagableLayer);
        var allies = TeemsManager.Instance.GetAllies(gameObject);//new List<Health>(combatSystem.Allies).ConvertAll(x => x.gameObject);

        for (int i = 0; i < damagableObject.Length; i++)
        {
            if (!allies.Contains(damagableObject[i].gameObject))
            {
                currentTargetEnemy = damagableObject[i].gameObject;
                Debug.Log("CurrentTarget", currentTargetEnemy);
                return true;
            }
        }
        currentTargetEnemy = null;
        Debug.Log("CurrentTarget", currentTargetEnemy);
        return false;
    }
    public bool RandomPoint(out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = transform.position + UnityEngine.Random.insideUnitSphere * searchRadiusPoint;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    public void ChangeState(StateBehaviour state)
    {
        states[currentStateCharacter].Exit();

        currentStateCharacter = state;

        states[currentStateCharacter].Enter();
    }
    protected void SetDefaultState()
    {
        currentStateCharacter = StateBehaviour.Idle;
        states[currentStateCharacter].Enter();
    }
    public void ResetTarget()
    {
        currentTargetEnemy = null;
    }
    public abstract void InitState();
}

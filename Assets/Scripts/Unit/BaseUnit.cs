using UnityEngine;
using UnityEngine.AI;

public abstract class BaseUnit : MonoBehaviour, IUnit
{
    public abstract float Damage { get; }
    public abstract float AttackRange { get; }
    public abstract float FireRate { get; }

    protected float attackTimer = 0f;
    protected bool isAttacking = false;

    [SerializeField] protected float health;
    protected NavMeshAgent myAgent;
    protected EnemyBuilding target;

    protected virtual void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            HandleTargetInteraction();
        }
    }

    protected void HandleTargetInteraction()
    {
        if (target != null && target.health <= 0)
        {
            target = null;
            isAttacking = false;
            myAgent.isStopped = true;
            return;
        }

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= AttackRange)
            {
                StartAttacking();
                if (isAttacking)
                {
                    Attack();
                }
            }
            else
            {
                StopAttacking();
                MoveTowardsTarget(target);
            }

            FaceTarget();
        }
    }


    protected abstract void Attack();

    public void MoveTowardsTarget(EnemyBuilding target)
    {
        if (target != null)
        {
            myAgent.isStopped = false;
            myAgent.SetDestination(target.transform.position);
        }
    }

    protected void FaceTarget()
    {
        if (target == null) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void StopAction()
    {
        target = null;
        isAttacking = false;
        myAgent.isStopped = false;
    }

    public void SetTarget(EnemyBuilding newTarget)
    {
        if (newTarget != null && newTarget.health <= 0)
        {
            newTarget = null;
        }

        if (target != newTarget)
        {
            StopAction();
            target = newTarget;
        }
    }

    public void ResetState()
    {
        myAgent.isStopped = false;
        isAttacking = false;
    }

    protected void StartAttacking()
    {
        isAttacking = true;
        myAgent.isStopped = true;
    }

    protected void StopAttacking()
    {
        isAttacking = false;
        myAgent.isStopped = false;
    }

    public void ResetUnitForNewCommand()
    {
        myAgent.isStopped = false;
        myAgent.ResetPath();
    }

}

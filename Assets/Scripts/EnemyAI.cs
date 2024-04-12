using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public PlayerController player;
    public float viewAngle;
    public float damage = 30;
    public float attackDistance = 1;
    public Animator animator;

    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;

    public bool IsAlive()
    {
        return _enemyHealth.IsAlive();
    }

    void Start()
    {
        InitComponentLinks();
        PickNewPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {

        NoticePlayerUpdate();
        ChaseUpdate();
        AttackUpdate();
        PatrolUpdate();
    }

    private void NoticePlayerUpdate()
    {
        _isPlayerNoticed = false;
        if (!_playerHealth.IsAlive()) return;
        var direction = player.transform.position - transform.position;

        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
            }
        }
    }

    private void ChaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }

    }

    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerHealth = player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();


    }

    private void PatrolUpdate()
    {
        if(!_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                PickNewPatrolPoint();
            }
        }

    }
    private void PickNewPatrolPoint()
    {
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }

    private void AttackUpdate()
    {
        if (_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                animator.SetTrigger("attack");

                //перманентный урон в секунду
                //_playerHealth.DealDamage(damage * Time.deltaTime);
            }
        }

    }

    public void AttackDamage()
    {
        if (!_isPlayerNoticed) return;
        if (_navMeshAgent.remainingDistance > (_navMeshAgent.stoppingDistance + attackDistance)) return;

        _playerHealth.DealDamage(damage);
    }
}

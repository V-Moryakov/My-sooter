using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float value = 100;
    public Animator animator;

    public PlayerProgress playerProgress;

    public CollectableItem collectablePrefab;

    private void Start()
    {
        playerProgress = FindObjectOfType<PlayerProgress>();
    }
    public bool IsAlive()
    {
        return value > 0;
    }

    public void DealDamage(float damage)
    {
        playerProgress.AddExperience(damage);
        value -= damage;
        if(value <= 0)
        {
            EnemyDeath();
            //Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }

    private void EnemyDeath()
    {
        animator.SetTrigger("death");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        MobExplosion();
    }

    private void MobExplosion()
    {
        if (collectablePrefab == null) return;
        var ciilectable = Instantiate(collectablePrefab);
        var coinposition = new Vector3(0, 1, 0);
        ciilectable.transform.position = transform.position + coinposition;
    }
}

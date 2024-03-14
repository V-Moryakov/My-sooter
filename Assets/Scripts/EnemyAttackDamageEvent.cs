using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDamageEvent : MonoBehaviour
{
    public EnemyAI enemyAI;
    public void AttackDamageEvent()
    {
        //Debug.Log("AAAAAAAAAAAAA");
        enemyAI.AttackDamage();
    }
}

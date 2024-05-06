using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDamage : MonoBehaviour
{
    public float damage = 1f;
    public Player_Health playerHealth;
    public float attackCooldown = 2f;

    float lastAttackTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if we took damage recently
        if (Time.time - lastAttackTime < attackCooldown) return;


        
        if(collision.gameObject.CompareTag("Player")) 
        {
            if (playerHealth.isHidden == false)
            {
                playerHealth.TakeDamage(damage);

                // Note when the player took damage
                lastAttackTime = Time.time;
            }

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDamage : MonoBehaviour
{
    public float damage = 1f;
    public Player_Health playerHealth;
    public float attackCooldown = 2f;

    public Player_Health pH;
    public GameObject playerSprite;
    public GameObject flashRed;
    float lastDamageTime;
    public float damageCooldown = 2f;

    float lastAttackTime;


        void TakeDamage ()
        {

            if (pH.isHidden == false) //Player can't take damage while being hidden
            {
                if (Time.time - lastDamageTime < damageCooldown && pH.health > 0) return;

                pH.health -= damage;
                StartCoroutine(Hurt());
                lastDamageTime = Time.time;

                if (pH.health <= 0)
                {
                    pH.isAlive = false;
                    flashRed.SetActive(true);
                    playerSprite.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    pH.isAlive = true;
                }
            }
        }


    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if we took damage recently
        if (Time.time - lastAttackTime < attackCooldown) return;

        
        if(collision.gameObject.CompareTag("Player")) 
        {
            if (pH.isHidden == false)
            {
                TakeDamage();

                // Note when the player took damage
                lastAttackTime = Time.time;
            }

        }
    }

    IEnumerator Hurt()
    {
        flashRed.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        flashRed.SetActive(false);
    }
}
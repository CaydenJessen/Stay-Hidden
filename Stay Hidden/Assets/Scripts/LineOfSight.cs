using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public bool isChasing = false;
    public bool lost = false;
    public float rayDirection = 1f;
    public float lineOfSightDistance = 2f;
    public GameObject ray;
    public EnemyAI EA;

    public enum enemstate{patrol, chase};
    public enemstate currentState;




    public Player_Health playerHealth;
    public float damage = 1f;
    public float attackCooldown = 2f;
    float lastAttackTime;

    void Update()
    {
       

        RaycastHit2D hit = Physics2D.Raycast(ray.transform.position, new Vector2(rayDirection, 0f), lineOfSightDistance);
        if (hit.collider.tag == "Player")
        {
            Debug.DrawRay(ray.transform.position, hit.distance * new Vector2(rayDirection, 0f), Color.red);  
            isChasing = true;
            Debug.Log("hit");
            currentState = enemstate.chase;
            
          // if (playerHealth.isHidden == false);
          // {
          //     playerHealth.TakeDamage(damage);

          //     // Note when the player took damage
          //     lastAttackTime = Time.time;
          // }
        }
        else
        {
            Debug.DrawRay(ray.transform.position, hit.distance * new Vector2(rayDirection, 0f), Color.green);
            isChasing = false;
            lost = true;
            Debug.Log("not hit");
            currentState = enemstate.patrol;
        }

        


    }
}

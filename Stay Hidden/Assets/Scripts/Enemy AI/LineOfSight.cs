using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public bool isChasing = false;
    public float rayDirection = 1f;
    public float lineOfSightDistance = 2f;
    public GameObject ray;

    public enum enemstate{patrol, chase};
    public enemstate currentState;

    public float attackCooldown = 2f;

    public Player_Health pH;

    public bool hitPlayer = false;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(ray.transform.position, new Vector2(-rayDirection, 0f), lineOfSightDistance);
        
        if (hit.collider.tag == "Player" && pH.isHidden == false )
        {
            Debug.DrawRay(ray.transform.position, hit.distance * new Vector2(-rayDirection, 0f), Color.red);  
            isChasing = true;
            Debug.Log("hit");
            currentState = enemstate.chase;
            hitPlayer = true;
        }
        else
        {
            Debug.DrawRay(ray.transform.position, hit.distance * new Vector2(-rayDirection, 0f), Color.green);
            isChasing = false;
            currentState = enemstate.patrol;
            hitPlayer = false;
        }

        if (pH.isHidden == true)
        {
            isChasing = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaWall : MonoBehaviour
{
    public float rayDirection = 1f;
    public float lineOfSightDistance = 2f;
    public GameObject ray;

    public enum enemstate{patrol, chase};
    public enemstate currentState;

    public LineOfSight LOS;

    public bool hitWall = false;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(ray.transform.position, new Vector2(-rayDirection, 0f), lineOfSightDistance);
        
        if ((hit.collider.tag == "Wall") || (hit.collider.tag == "Ground"))
        {
            Debug.DrawRay(ray.transform.position, hit.distance * new Vector2(-rayDirection, 0f), Color.red);  
            LOS.isChasing = false;
            Debug.Log("hit");
            currentState = enemstate.patrol;
            hitWall = true;
        }
        else
        {
            hitWall = false;
        }
    }
}

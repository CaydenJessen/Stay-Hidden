using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D enemy;
    private Transform targetPoint;
    public float walkSpeed = 3f;
    public float targetSize = 1f;
    private float idleSpeed = 0f;
    private float speed;
    public float wait = 3.0f;
    public Transform player;
    public float chaseSpeed = 5f;
    public LineOfSight lOS;
    public bool isFacingRight = false;
    public bool isFacingLeft = false;
    public float transformData;

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        enemy = GetComponent<Rigidbody2D>();
        targetPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (lOS.isChasing == true)
        {
            lOS.lost = false;
            Debug.Log("chase is true");
            Chase();
        }
      else if(lOS.lost == true)
        {
            lOS.lost = false;
            Debug.Log("target lost");
            targetPoint = pointA.transform;
            //StartCoroutine(Confused());

        }
       
       if(lOS.currentState == LineOfSight.enemstate.patrol)
       {
            Patrol();
       }
     
    }

    void Patrol()
    {
        Vector2 point = targetPoint.position - transform.position;
        if (targetPoint == pointB.transform)
        {
            

            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, step);
            if(transform.position.x < pointB.transform.position.x)
            {
                isFacingRight = true;
                Debug.Log("going right");
                isFacingLeft = false;
            }
            else
            {
                isFacingRight = false;
            }

        }
        else if (targetPoint == pointA.transform)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, step);
            if (transform.position.x > pointA.transform.position.x)
            {
                isFacingLeft = true;
                Debug.Log("going left");
                isFacingRight = false;
            }
            else
            {
                isFacingLeft = false;
            }
        }
        if (Vector2.Distance(transform.position, targetPoint.position) < targetSize && targetPoint == pointB.transform)
        {
            speed = idleSpeed;
            StartCoroutine(Idle());
            targetPoint = pointA.transform;

        }
        if (Vector2.Distance(transform.position, targetPoint.position) < targetSize && targetPoint == pointA.transform)
        {
            speed = idleSpeed;
            StartCoroutine(Idle());
            targetPoint = pointB.transform;
            
 
        }
      
    }

    void Direction()
    {
        if (isFacingRight == true)
        {
            transform.localScale = new Vector3(transformData, transformData, transformData);
            Debug.Log("flipped to right");
            isFacingRight = false;
            lOS.rayDirection = -1f;

        }
        if (isFacingLeft == true)
        {
            transform.localScale = new Vector3(-transformData, transformData, transformData);
            Debug.Log("flipped to left");
            isFacingLeft = false;
            lOS.rayDirection = 1f;

        }
    }
    void Chase()
    {
            if(transform.position.x > player.position.x)
            {
                transform.localScale = new Vector3(-transformData, transformData, transformData);
                transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
            }
            if (transform.position.x < player.position.x)
            {
                transform.localScale = new Vector3(transformData, transformData, transformData);
                transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
            }

    }


    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointA.transform.position, targetSize);
        Gizmos.DrawWireSphere(pointB.transform.position, targetSize);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
   
    IEnumerator Idle()
    {
        Debug.Log("Idle");
        yield return new WaitForSeconds(wait);
        speed = walkSpeed;
        Direction();
    }


    IEnumerator Confused()
    {
        speed = idleSpeed;
        yield return new WaitForSeconds(2);
        flip();
        yield return new WaitForSeconds(1);
        flip();
        yield return new WaitForSeconds(1);
        flip();
        lOS.lost = false;
        speed = walkSpeed;
        targetPoint = pointA.transform;
        Debug.Log("back to patrol");
    }
}
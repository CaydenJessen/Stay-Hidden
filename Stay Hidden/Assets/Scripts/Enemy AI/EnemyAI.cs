using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D enemy;
    private Animator anim;
    private Transform targetPoint;
    public Transform player;
    public LineOfSight lOS;
    public Animator animator;
    public Player_Health pH;

    public float speed;
    public float walkSpeed = 3f;
    public float targetSize = 1f;
    private float idleSpeed = 0f;
    public float chaseSpeed = 5f;

    public bool isFacingRight = false;
    public bool movingRight = true;
    
    public float wait = 3.0f;
    public bool lost = true;
    public bool lightAlert = false;
    public bool canWalk = true;
    public bool wallCollide = false;
    
    public float oldPos;
    public float newPos;

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        enemy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        targetPoint = pointB.transform;
        oldPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(canWalk == true)
        {
            animator.SetFloat("Speed", speed);
            if (lOS.isChasing == true || lightAlert == true)
            {
                lost = false;
                Debug.Log("chase is true");
                Chase();
            }

            else if(lost == true)
            {
                lost = false;
                lOS.isChasing = false;
                Debug.Log("target lost");
                targetPoint = pointA.transform;
                //StartCoroutine(Confused());
            }


            if((lOS.hitPlayer == true) && (lOS.isChasing == false))
            {
                lOS.hitPlayer = false;
                // StartCoroutine(Confused());
            }
            
            if((lOS.isChasing == false) || (pH.isHidden == true))
            {
                Patrol();
            }
        }

        CheckPosition();
    }


    //CHECK IF MOVING LEFT OR RIGHT BASED ON X POSITION OF THE ENEMY
    void CheckPosition()
    {
        newPos = transform.position.x;

        if(newPos < oldPos)
        {
            isFacingRight = false;
            movingRight = false;

            Vector3 localScale = transform.localScale;
            localScale.x = -1;
            transform.localScale = localScale;
            
        }
        if(newPos > oldPos)
        {
            isFacingRight = true;
            movingRight = true;

            Vector3 localScale = transform.localScale;
            localScale.x = 1;
            transform.localScale = localScale;
            
        }
        oldPos = newPos;
        
        Direction();
    }


    void Patrol()
    {
        Vector2 point = targetPoint.position - transform.position;
        if (targetPoint == pointB.transform)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, step);
        }
        else if (targetPoint == pointA.transform)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, step);
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


    void Direction() //FLIPS THE DIRECTION OF THE LINE OF SIGHT RAYCAST
    {
        if ((isFacingRight == true) || (movingRight == true))
        {
            lOS.rayDirection = -1f;
        }
        else
        {
            if ((isFacingRight == false) || (movingRight == false))
            {
                lOS.rayDirection = 1f;
            }
        }
    }


    void Chase() //Moves the enemy to the direction of the player if the enemy is chasing
    {
        if(pH.isHidden == false)
        {
            if(transform.position.x > player.position.x)
            {
                transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
            }
            if (transform.position.x < player.position.x)
            {
                transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointA.transform.position, targetSize);
        Gizmos.DrawWireSphere(pointB.transform.position, targetSize);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
   

    IEnumerator Idle() //Set when the enemy is not chasing
    {
        //Debug.Log("Idle");
        yield return new WaitForSeconds(wait);
        speed = walkSpeed;
        Direction();
    }

    //StopCoroutine(Confused())!!!!!!!!!!!

    // IEnumerator Confused()
    // {
    //     speed = idleSpeed;
    //     yield return new WaitForSeconds(2);
    //     flip();
    //     yield return new WaitForSeconds(1);
    //     flip();
    //     yield return new WaitForSeconds(1);
    //     flip();
    //     lost = false;
    //     speed = walkSpeed;
    //     targetPoint = pointA.transform;
    //     Debug.Log("back to patrol");
    // }

    private void flip() //FLIPS THE ENEMY SPRITE
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


    private void OnCollisionEnter2D(Collision2D touchPlayer)
    {
        if((touchPlayer.gameObject.CompareTag("Player")))
        {
            canWalk = false;
        }

        if ((touchPlayer.gameObject.tag == "Wall") || (touchPlayer.gameObject.tag == "Enemy"))
        {
            lOS.isChasing = false;
            Patrol();

            flip();
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("Wall")) || (col.gameObject.tag == "Darkness"))
        {
            wallCollide = true;
            lOS.isChasing = false;
            speed = idleSpeed;
            StartCoroutine(Idle());
            flip();
        }
    }


    private void OnCollisionExit2D(Collision2D notouchPlayer)
    {
        if((notouchPlayer.gameObject.CompareTag("Player")))
        {
            canWalk = true; //Enemy can't push the player
        }
    }


    void OnTriggerExit2D(Collider2D nocol)
    {
        if ((nocol.gameObject.CompareTag("Wall")) || (nocol.gameObject.tag == "Darkness"))
        {
            wallCollide = false;
        }
    }
}
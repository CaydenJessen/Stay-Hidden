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
    public float walkSpeed = 3f;
    public float targetSize = 1f;
    private float idleSpeed = 0f;
    public float speed;
    public float wait = 3.0f;
    public Transform player;
    public float chaseSpeed = 5f;
    public LineOfSight lOS;
    public bool isFacingRight = false;
    public bool isFacingLeft = false;
    public bool chase = false;

    public bool lost = false;

    public Animator animator;

    public bool canWalk = true;
    
    public float distance;
    public bool movingRight = true;
    public Transform groundDetection;
    [SerializeField] Transform wallDetection;
    [SerializeField] LayerMask wallLayerMask;

    public bool pointController;

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        enemy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        targetPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(canWalk == true)
        {
            animator.SetFloat("Speed", speed);
            if (lOS.isChasing == true || chase == true)
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
            
            
            if(lOS.isChasing == false)
            {
                    Patrol();
            }
        }
    }





    void Patrol()
    {
        //----------PATROL BETWEEN 2 PPOINTS-------------//
        if(pointController == true)
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
                flip();
                StartCoroutine(Idle());
                targetPoint = pointA.transform;

            }
            if (Vector2.Distance(transform.position, targetPoint.position) < targetSize && targetPoint == pointA.transform)
            {
                speed = idleSpeed;
                flip();
                StartCoroutine(Idle());
                targetPoint = pointB.transform;
            }
        }
        //END OF OLD PATROL//
        else
        {
            //-------------NEW PATROL----------//
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
            if (groundInfo.collider == false)
            {
            if(movingRight==true)
                {
                    Vector3 origin = wallDetection.position;
                    Vector3 dir = Vector2.right;
                    RaycastHit2D hit = Physics2D.Raycast( origin , dir , distance , wallLayerMask );
                    if( hit.collider!=null )
                    {
                        movingRight = false;
                        Debug.DrawLine( origin , hit.point , Color.red );
                    }
                    else Debug.DrawLine( origin , origin + dir*distance , Color.white , 0.01f );
                }
                else
                {
                    Vector3 origin = wallDetection.position;
                    Vector3 dir = -Vector2.right;
                    RaycastHit2D hit = Physics2D.Raycast( origin , dir , distance , wallLayerMask );
                    if( hit.collider!=null )
                    {
                        movingRight = true;
                        Debug.DrawLine( origin , hit.point , Color.red );
                    }
                    else Debug.DrawLine( origin , origin + dir*distance , Color.white , 0.01f );
                }
            }
        }
    }









    void Direction()
    {
        if ((isFacingRight == true) || (movingRight == true))
        {
            Debug.Log("flipped to right");
            isFacingRight = false;
            lOS.rayDirection = -1f;
        }
        if ((isFacingLeft == true) || (movingRight == false))
        {
            Debug.Log("flipped to left");
            isFacingLeft = false;
            lOS.rayDirection = 1f;
        }
    }
    void Chase()
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
        lost = false;
        speed = walkSpeed;
        targetPoint = pointA.transform;
        Debug.Log("back to patrol");
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
            //Debug.Log("HIT THE WALLL!!!!");
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("Wall")) || (col.gameObject.tag == "Darkness"))
        {
            speed = idleSpeed;
            StartCoroutine(Idle());
            Debug.Log("E");
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }





    private void OnCollisionExit2D(Collision2D notouchPlayer)
    {
        if((notouchPlayer.gameObject.CompareTag("Player")))
        {
            canWalk = true;
        }
    }

}






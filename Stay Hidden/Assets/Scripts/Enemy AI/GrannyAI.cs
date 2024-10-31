using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyAI : MonoBehaviour
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

    public Player_Movement pM;

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

    public float damage;

    public bool transformOne = false;
    public bool transformTwo = false;
    public bool transformThree = false;
    public bool transformFour = false;
    public bool transformFive = false;


    public GameObject LightOne;
    public GameObject LightTwo;
    public GameObject LightThree;
    public GameObject LightFour;
    public GameObject LightFive;

    public GrannyLights glOne;
    public GrannyLights glTwo;
    public GrannyLights glThree;
    public GrannyLights glFour;
    public GrannyLights glFive;

    private Transform lightPoint;



    public Deposit dep;

    public float damageIncrease = 1;

    public EnemyDamage eD;

    public LineOfSight LOS;
    public float vision;

    public Animator animate;

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
            if (lOS.isChasing == true)
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
            
            if(((lOS.isChasing == false) || (pH.isHidden == true)) && (pH.inLight == false))
            {
                Patrol();
            }
        
            if (pH.inLight == true)
            {
                Lights();
            }
            
        }

        CheckPosition();

        Transform();
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

    void Transform() 
    {
        //SECOND FORM
        if ((pM.num + dep.count == 0) && (transformOne == false))
        {
            transformOne = true;
            animate.SetBool("T1", true);
            walkSpeed = walkSpeed + 1f;
            chaseSpeed = chaseSpeed + 1f;
            eD.damage = eD.damage + damageIncrease;
            LOS.lineOfSightDistance = LOS.lineOfSightDistance + vision;
            //CHANGE SPRITE TO NEXT FORM
        }

        //THIRD FORM
        if ((pM.num + dep.count == 1) && (transformTwo == false))
        
        {
            transformTwo = true;
            animate.SetBool("T2", true);
            walkSpeed = walkSpeed + 1f;
            chaseSpeed = chaseSpeed + 1f;
            eD.damage = eD.damage + damageIncrease;
            LOS.lineOfSightDistance = LOS.lineOfSightDistance + vision;
            //CHANGE SPRITE TO NEXT FORM
        }

        //FOURTH FORM
        if ((pM.num + dep.count == 2) && (transformThree == false))
        {
            transformThree = true;
            animate.SetBool("T3", true);
            walkSpeed = walkSpeed + 1f;
            chaseSpeed = chaseSpeed + 1f;
            eD.damage = eD.damage + damageIncrease;
            LOS.lineOfSightDistance = LOS.lineOfSightDistance + vision;
            //CHANGE SPRITE TO NEXT FORM
        }

        //TRANSFORMING STATE (NOT MOVING)!!!!
        if ((pM.num + dep.count == 3) && (transformFour == false))
        {
            transformFour = true;
            animate.SetBool("T4", true);
            walkSpeed = 0f;
            chaseSpeed = 0f;
            //CHANGE SPRITE TO NEXT FORM
        }

        //FINAL FORM!!!!
        if ((pM.num + dep.count == 4) && (transformFive == false))
        {
            transformFive = true;
            animate.SetBool("T5", true);
            walkSpeed = 5f; //Walk speed = 5
            chaseSpeed = 7f; //Chase speed = 7
            eD.damage = eD.damage + damageIncrease;
            LOS.lineOfSightDistance = LOS.lineOfSightDistance + vision;
            speed = walkSpeed;
            //ANIMATION TO TRANSFORM INTO FINAL FORM !!!!
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


    void Lights() //Moves the enemy to the direction of the player if the enemy is chasing
    {
        if((lOS.isChasing == false) && (pH.inLight == true))
        {
            if(glOne.ligthCollide == true)
            {
                lightPoint = LightOne.transform;
            }

            if(glTwo.ligthCollide == true)
            {
                lightPoint = LightTwo.transform;
            }

            if(glThree.ligthCollide == true)
            {
                lightPoint = LightThree.transform;
            }

            if(glFour.ligthCollide == true)
            {
                lightPoint = LightFour.transform;
            }

            if(glFive.ligthCollide == true)
            {
                lightPoint = LightFive.transform;
            }


            Vector2 point = lightPoint.position - transform.position;
            if (Vector2.Distance(transform.position, lightPoint.position) < targetSize)
            {
                speed = idleSpeed;
                StartCoroutine(Idle());
            }
            else
            {
                var step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);
            }
        }
    }



}
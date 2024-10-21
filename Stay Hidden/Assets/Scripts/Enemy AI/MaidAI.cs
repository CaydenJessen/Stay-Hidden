using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidAI : MonoBehaviour
{
    private Rigidbody2D enemy;
    private Animator anim;
    public Transform player;
    public LineOfSight lOS;
    public Animator animator;
    public Player_Health pH;
    public MamaAI Mama;

    public float speed;
    public float idleSpeed = 0f;
    public float chaseSpeed = 5f;

    public bool isFacingRight = false;
    public bool movingRight = true;
    
    public float wait = 3.0f;
    public bool lost = true;
    public bool canWalk = true;
    public bool wallCollide = false;
    
    public float oldPos;
    public float newPos;

    public bool CHASINGNOW = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = chaseSpeed;
        enemy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        oldPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(canWalk == true)
        {
            animator.SetFloat("Speed", speed);
            //if (lOS.isChasing == true)
            if(Mama.mamaChase == true)
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
                //StartCoroutine(Confused());
            }
            

            if((lOS.hitPlayer == true) && (lOS.isChasing == false))
            {
                lOS.hitPlayer = false;
                StartCoroutine(Confused());
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



    void Direction() //FLIPS THE DIRECTION OF THE LINE OF SIGHT RAYCAST
    {
        if ((isFacingRight == true) || (movingRight == true))
        {
            //Debug.Log("flipped to right");
            lOS.rayDirection = -1f;
        }
        else
        {
            if ((isFacingRight == false) || (movingRight == false))
            {
                // Debug.Log("flipped to left");
                lOS.rayDirection = 1f;
            }
        }
    }


    void Chase() //Moves the enemy to the direction of the player if the enemy is chasing
    {
        CHASINGNOW = true;
        if(transform.position.x > player.position.x)
        {
            transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
        }
        if (transform.position.x < player.position.x)
        {
            transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
        }
    }
   

    IEnumerator Idle() //Set when the enemy is not chasing
    {
        //Debug.Log("Idle");
        yield return new WaitForSeconds(wait);
        Direction();
    }

    //StopCoroutine(Confused())!!!!!!!!!!!

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
        Debug.Log("back to patrol");
    }

    private void flip() //FLIPS THE ENEMY SPRITE FOR TYPE 1 PATROL
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

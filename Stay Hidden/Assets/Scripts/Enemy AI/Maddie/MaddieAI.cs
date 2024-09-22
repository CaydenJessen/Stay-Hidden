using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaddieAI : MonoBehaviour
{
    private Rigidbody2D enemy;
    private Animator anim;
    public Transform player;
    public Animator animator;
    public Player_Health pH;

    public float speed;
    public float targetSize = 1f;
    private float idleSpeed = 0f;
    public float chaseSpeed = 5f;

    public bool isFacingRight = false;
    public bool movingRight = true;
    
    public float wait = 3.0f;
    public bool lost = true;
    public bool canWalk = true;
    
    public float oldPos;
    public float newPos;
    
    public bool deSpawn = false;
    public GameObject Maddie;
    public bool chasePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = idleSpeed;
        enemy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        oldPos = transform.position.x;
        Maddie.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(chasePlayer == false)
        {
            speed = idleSpeed;
        }
        else
        {
            if((chasePlayer == true) && (canWalk == true) && (deSpawn == false))
            {
                Chase();
            }
        }

        CheckPosition();
    }



//CHECK IF MOVING LEFT OR RIGHT BASED ON X POSITION OF THE ENEMY
//This method works but conflicts with the methods below
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
    }



    void Chase() //Moves the enemy to the direction of the player if the enemy is chasing
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
            chasePlayer = false;
            deSpawn = true;
            Maddie.SetActive(false);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("Wall")) || (col.gameObject.tag == "Darkness"))
        {
            chasePlayer = false;
            speed = idleSpeed;
            deSpawn = true;
            Maddie.SetActive(false);
        }
    }



    private void OnCollisionExit2D(Collision2D notouchPlayer)
    {
        if((notouchPlayer.gameObject.CompareTag("Player")))
        {
            canWalk = true; //Enemy can't push the player
        }
    }






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
}
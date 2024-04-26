using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jump = 4f;
    public float runSpeedMultiplier = 1.5f;
    public float currentSpeed;

    public bool isFacingRight = false;
    public bool canJump = true; 
    bool isRunning = false;
    public bool tailControl;
    public Animator animator;

    public GameObject Player;

    public Player_Health pH;
    public float timer = 0;
    public float skillTime = 5;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentSpeed = 0f;
            tailControl = true;
        }
        else
        {
            currentSpeed = speed;
            tailControl = false;
        }
        //Store horizontal value
        horizontal = Input.GetAxis("Horizontal");

        //Flip Spite when changing direction
        FlipSprite();


        //Jump
        if(Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isRunning = false; //Player stops running when they jump / stops momentum
        }


        //Sprint detection
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canJump == true) //Player cant run mid air after jumping
            {
                isRunning = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        HidingMechanic();
        
    

    }


    private void FixedUpdate()
    {
        //Player walk movement
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);

        //Increase player speed if they run / press shift
        if (isRunning && tailControl == false)
        {
            animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
            rb.velocity = new Vector2(horizontal * (speed * runSpeedMultiplier), rb.velocity.y);
        }

    }

    //Flip sprite so it faces direction it is running
    void FlipSprite()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }


    //Check if player is colliding with ground == Jump allowed
    private void OnCollisionEnter2D(Collision2D onGround)
    {
        if((onGround.gameObject.CompareTag("Ground")) || (onGround.gameObject.CompareTag("Enemy")))
        {
            canJump = true;
        }

    }
    private void OnCollisionExit2D(Collision2D offGround)
    {
        if((offGround.gameObject.CompareTag("Ground")) || (offGround.gameObject.CompareTag("Enemy")))
        {
            canJump = false;
        }
    }

    private void HidingMechanic()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) && timer < skillTime)
        {    
            timer += Time.deltaTime;

            if (timer >= skillTime)
		    {
			    pH.isHidden = true;
		    }
            
            Debug.Log("Crouch Hiding");
        }
        
        if(Input.GetKeyUp(KeyCode.LeftControl) && timer >= skillTime && pH.isHidden == false)
        {
            pH.isHidden = false;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && timer >= skillTime)
        {
            pH.isHidden = false;

        }
        if(timer >=5 )
        {
            pH.isHidden = false;
        }
        if (pH.isHidden == true)
        {
            Player.transform.localScale = new Vector3(1.862f, 0.3f , 1.862f);
        }
        if(pH.isHidden == false && isFacingRight == false)
        {
            
            Player.transform.localScale = new Vector3(1.862f, 1.862f , 1.862f);
        }
        else if(pH.isHidden == false && isFacingRight == true)
        {
            Player.transform.localScale = new Vector3(-1.862f, 1.862f , 1.862f);
        }
        if(pH.isHidden == false && timer > 0)
        {
            timer -= Time.deltaTime;
        }
      
         
    }
   

}
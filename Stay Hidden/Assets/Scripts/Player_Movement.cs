using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jump = 4f;
    public float runSpeedMultiplier = 1.5f;
    public float currentSpeed;

    public bool isFacingRight = false;
    public bool canJump = false;
    public bool touchGround = false;
    bool isRunning = false;
    public bool tailControl;
    public Animator animator;

    public GameObject Player;

    public Player_Health pH;

    public float stamina;
    public float maxStamina;
    public float skillCost;
    public Image staminaBar;
    public float chargeRate;
    private Coroutine recharge;
    public bool lit;
    public bool canHide = false;


    public bool hasItem = false;
    public int num = -1;

    Rigidbody2D rb;
    public float colliderX;
    public float colliderY;

    public bool camResize = false;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;


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


        //Jump using Collision
        // if(Input.GetButtonDown("Jump") && canJump)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jump);
        //     //isRunning = false; //Player stops running when they jump / stops momentum
        // }

        //Jump using Raycasting
        if(Input.GetButtonDown("Jump") && isGrounded() && pH.isAlive == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            //isRunning = false; //Player stops running when they jump / stops momentum
        }


        //Sprint detection
        if (Input.GetKeyDown(KeyCode.LeftShift) && pH.isAlive == true)
        {
            if (isGrounded()) //Player cant run mid air after jumping
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
        if (pH.isAlive == true)
        {
            rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);

        }
        //Player walk movement
        //rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);

        //Increase player speed if they run / press shift
        if ((isRunning && tailControl == false) && (pH.isAlive == true))
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


    //Ground Check using Raycasting
    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);

    }

    


    // //Ground Check Using Collision
    // private void OnCollisionEnter2D(Collision2D onGround)
    // {
    //     if((onGround.gameObject.CompareTag("Ground")) || (onGround.gameObject.CompareTag("Enemy")))
    //     {
    //         canJump = true;
    //         canHide = true;
    //         touchGround = true;
    //     }
    //     else
    //     {
    //         if (onGround.gameObject.CompareTag("Moving Platform"))
    //         {
    //             canHide = false;
    //             canJump = true;
    //             touchGround = true;
    //         }
    //         else
    //         {
    //             canHide = false;
    //             canJump = false;
    //             touchGround = false;
    //         }
    //     }



    //     if (onGround.gameObject.CompareTag("Moving Platform"))
    //     {
    //         canHide = false;
    //         canJump = true;
    //         touchGround = true;
    //     }


    // }
    // private void OnCollisionExit2D(Collision2D offGround)
    // {
    //     if((offGround.gameObject.CompareTag("Ground")) || (offGround.gameObject.CompareTag("Enemy")))
    //     {
    //         canJump = false;
    //         touchGround = false;

    //     }
    //     if (offGround.gameObject.CompareTag("Moving Platform"))
    //     {
    //         canHide = true;
    //         canJump = false;
    //         touchGround = false;
    //     }
    // }




    private void HidingMechanic()
    {
       
        if(Input.GetKey(KeyCode.LeftControl) && canHide == true && pH.inDarkness == false)
        {    
            Player.transform.localScale = new Vector3(1.862f, 0.3f , 1.862f);
            GetComponent<BoxCollider2D>().size = new Vector2(colliderX, 0.05f);
            canJump = false;
           
            isRunning = false;

            pH.isHidden = true;

            stamina -= skillCost * Time.deltaTime; 
            if (stamina < 0)
            {
                stamina = 0;
            }
            staminaBar.fillAmount = stamina / maxStamina;

            
            if(recharge != null)
            {
                StopCoroutine(recharge);
            }
            recharge = StartCoroutine(RechargeStamina());

        }
        



        //--------------PLAYER IS ABLE TO JUMP INFINITELY BUG---------------
        if(Input.GetKeyUp(KeyCode.LeftControl) && isFacingRight == false && canHide == true || stamina == 0 && isFacingRight == false && canHide == true || lit == true && isFacingRight == false && canHide == true && (pH.inDarkness == false))
        {
            pH.isHidden = false;
            Player.transform.localScale = new Vector3(1.862f, 1.862f , 1.862f);
            GetComponent<BoxCollider2D>().size = new Vector2(colliderX, colliderY);
            
            if(touchGround)
            {
                canJump = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && isFacingRight == true && canHide == true || stamina == 0 && isFacingRight == true && canHide == true || lit == true && isFacingRight == true && canHide == true && (pH.inDarkness == false))
        {
            pH.isHidden = false;
            Player.transform.localScale = new Vector3(-1.862f, 1.862f , 1.862f);
            GetComponent<BoxCollider2D>().size = new Vector2(colliderX, colliderY);
            
            if(touchGround)
            {
                canJump = true;
            }
        }
    }


    
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("Light")))
        {
            lit = true;
        }

        if (col.gameObject.tag == "Item")
        {
            Debug.Log("Item Taken");
            hasItem = true;
            num++;
        }
        if (col.gameObject.tag == "Cam Sizer")
        {
            camResize = true;
        }
       

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("Light")))
        {
            lit = false;
        }

        if (col.gameObject.tag == "Cam Sizer")
        {
            camResize = false;
        }
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds (2f);

        while(stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
        }
        if(stamina>maxStamina)
        {
            stamina = maxStamina;
            staminaBar.fillAmount = stamina/maxStamina;
            yield return new WaitForSeconds(0.1f);
        }
    }

}


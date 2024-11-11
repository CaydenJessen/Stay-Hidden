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
    public float idleSpeed = 0f;
    public float rechargeWait = 2f;


    public bool isFacingRight = false;
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

    public bool hasItem = false;
    public int num = -1;

    Rigidbody2D rb;
    public float colliderX;
    public float colliderY;
    public float offsetX;
    public float offsetY;
    public float originalColliderX;
    public float originalColliderY;
    public float originalOffsetX;
    public float originalOffsetY;

    public bool camResize = false;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    public Rigidbody2D playerRigid;  
    public float FallingThreshold = -10f;  
    public bool Falling = false; 
    public Pause pause;

    public bool viewing;
    public GameObject view;

    public bool Squeezing = false;

    public bool isRunning = false;
    public bool canCrouch = true;
    public bool canTail = true;
    public bool canHide = false;
    public bool canJump = false;
    float jumpTimeCounter;
    public float jumpTime;
    public float riseSpeed;

    public bool isJumping;

    public bool tailControl;
    public bool isCrouch = false;
    public bool isHiding;

    public bool Disengage = false;

    //  public class walk_loop {};
    // void Play();
    //  walk_loop audioData;
    // public walk_loop AudioData { get => audioData; set => audioData = value; }
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((canTail == false) || (isHiding == false))
        {
            animator.SetBool("Tail", false);
        }

        if (((Input.GetMouseButton(0) && canTail == true)) && (isHiding == false))
        {
            tailControl = true;
            animator.SetBool("Tail", true);
            canJump = false;
            currentSpeed = idleSpeed;
            canCrouch = false;
            //canHide = false;
        }
        else
        {
            //currentSpeed = speed;
            tailControl = false;
            animator.SetBool("Tail", false);
            canJump = true;
            canCrouch = true;
            //canHide = true;
        }
        if (pause.isPaused == true)
        {
            tailControl = false;
        }

        if (viewing == true)
        {
            view.SetActive(true);
        }
        else
        {
            view.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(InputDelay());
        }

        IEnumerator InputDelay()
        {
            currentSpeed = idleSpeed;
            yield return new WaitForSeconds (5f);
        }



        //Store horizontal value
        horizontal = Input.GetAxis("Horizontal");

        //Flip Spite when changing direction
        FlipSprite();
        if (playerRigid.velocity.y < FallingThreshold)
        {
            Falling = true;
            animator.SetBool("Falling", true);
        }
        else
        {
            Falling = false;
            animator.SetBool("Falling", false);
        }

        //Jump using Collision
        // if(Input.GetButtonDown("Jump") && canJump)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jump);
        //     //isRunning = false; //Player stops running when they jump / stops momentum
        // }


        //Jump using Raycasting
        if ((Input.GetButtonDown("Jump") && isGrounded() && pH.isAlive == true) && (canJump == true))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jump;
            animator.SetFloat("yVelocity", Mathf.Abs(rb.velocity.y));
        }

        if(Input.GetKey(KeyCode.Space) && pH.isAlive == true && isJumping == true && isHiding == false)
        {
            if(jumpTimeCounter > 0.15f)
            {
                rb.velocity = Vector2.up * riseSpeed;
                jumpTimeCounter -= Time.deltaTime;
                animator.SetFloat("yVelocity", Mathf.Abs(rb.velocity.y));
            }
            else
            {
                isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
           isJumping = false;
        }
    

        if(isGrounded())
        {
            animator.SetBool("Jump", false);

            animator.SetBool("Land", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jump", true);
        }

        if((pH.isHidden == true) && (pH.inDarkness == false))
        {
            animator.SetBool("Hiding", true);
        }
        else if(pH.isHidden == false)
        {
            animator.SetBool("Hiding", false);
        }

        if ((Input.GetKey(KeyCode.S)) && (tailControl == false) && (canCrouch == true))
        {
            canTail = false;
            currentSpeed = 0f;
            animator.SetBool("Crouch", true);
            canJump = false;
            //canHide = false;
        }
        else
        {
            if (tailControl == false)
            {
                canTail = true;
                currentSpeed = speed;
                animator.SetBool("Crouch", false);
                canJump = true;
                //canHide = true;
            }
        }

        if((Input.GetMouseButtonUp(1)) && Squeezing == false)
        {
            animator.SetBool("stopHide", true);
        }
        else
        {
            animator.SetBool("stopHide", false);
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

       if (Input.GetKeyDown(KeyCode.D))
       {
            //Debug.Log("footstep");
       //     audioData = GetComponent<walk_loop>();
       //     global::System.Object value = audioData.Play(4);
            
        }
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
            rb.velocity = new Vector2(horizontal * (currentSpeed * runSpeedMultiplier), rb.velocity.y);
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




    // Animation
    private void OnCollisionEnter2D(Collision2D onGround)
    {
        if ((onGround.gameObject.CompareTag("Ground")) || (onGround.gameObject.CompareTag("Enemy"))||(onGround.gameObject.CompareTag("Moving Platform")) || (onGround.gameObject.CompareTag("Box") || (onGround.gameObject.CompareTag("Damaging Environment"))))
        {
            animator.SetBool("Land", true);
        }
        else
        {

            animator.SetBool("Land", false);
        }

        if((onGround.gameObject.CompareTag("Moving Platform")))
        {
            canHide = false;
        }
    }


        private void OnCollisionExit2D(Collision2D offGround)
        {
            if((offGround.gameObject.CompareTag("Moving Platform")))
            {
                canHide = true;
            }
        }





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
        if(((Input.GetMouseButton(1) && canHide == true) && Squeezing == false) && (pH.inDarkness == false))
        {    
            GetComponent<BoxCollider2D>().size = new Vector2(colliderX, colliderY);
            GetComponent<BoxCollider2D>().offset = new Vector2(offsetX, offsetY);
            
            canJump = false;
            isRunning = false;
            canCrouch = false;
            canTail = false;

            pH.isHidden = true;
            isHiding = true;

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

        // if(Squeezing == true)
        // {
        //     GetComponent<BoxCollider2D>().size = new Vector2(colliderX, colliderY);
        //     GetComponent<BoxCollider2D>().offset = new Vector2(offsetX, offsetY);
        //     pH.isHidden = true;
        //     animator.SetBool("Hiding", true);
        // }
        // else
        // {
        //     pH.isHidden = false;
        // }
        



        //--------------PLAYER IS ABLE TO JUMP INFINITELY BUG---------------
        if(((Input.GetMouseButtonUp(1)) && isFacingRight == false && canHide == true || stamina == 0 && isFacingRight == false && canHide == true || lit == true && isFacingRight == false && canHide == true && (pH.inDarkness == false)) && Squeezing == false)
        {
            pH.isHidden = false;
            Player.transform.localScale = new Vector3(1.862f, 1.862f , 1.862f);
            GetComponent<BoxCollider2D>().size = new Vector2(originalColliderX, originalColliderY);
            GetComponent<BoxCollider2D>().offset = new Vector2(originalOffsetX, originalOffsetY);
            isHiding = false;

        }
        else if(((Input.GetMouseButtonUp(1)) && isFacingRight == true && canHide == true || stamina == 0 && isFacingRight == true && canHide == true || lit == true && isFacingRight == true && canHide == true && (pH.inDarkness == false)) && Squeezing == false)
        {
            pH.isHidden = false;
            Player.transform.localScale = new Vector3(-1.862f, 1.862f , 1.862f);
            GetComponent<BoxCollider2D>().size = new Vector2(originalColliderX, originalColliderY);
            GetComponent<BoxCollider2D>().offset = new Vector2(originalOffsetX, originalOffsetY);
            isHiding = false;
        }
    }





    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("Light")))
        {
            lit = true;
        }

        // if (col.gameObject.tag == "Item")
        // {
        //     Debug.Log("Item Taken");
        //     hasItem = true;
        //     num++;
        // }
        if (col.gameObject.tag == "Cam Sizer")
        {
            camResize = true;
        }
    }


    private void OnTriggerStay2D(Collider2D collision) 
    {
        if ((collision.gameObject.CompareTag("Light")))
        {
            pH.inLight = true;
        }
        if ((collision.gameObject.CompareTag("Squeeze")))
        {
            Squeezing = true;
            GetComponent<BoxCollider2D>().size = new Vector2(colliderX, colliderY);
            GetComponent<BoxCollider2D>().offset = new Vector2(offsetX, offsetY);
        }
        if ((collision.gameObject.CompareTag("Disengage")))
        {
            Disengage = true;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("Light")))
        {
            lit = false;
            pH.inLight = false;
        }

        if (col.gameObject.tag == "Cam Sizer")
        {
            camResize = false;
        }
        if ((col.gameObject.CompareTag("Squeeze")))
        {
            Squeezing = false;
            pH.isHidden = false;
            isHiding = false;
            StartCoroutine(AfterSqueeze());
            GetComponent<BoxCollider2D>().size = new Vector2(originalColliderX, originalColliderY);
            GetComponent<BoxCollider2D>().offset = new Vector2(originalOffsetX, originalOffsetY);
        }
        if (col.gameObject.tag == "Disengage")
        {
            Disengage = false;
        }
    }

    private IEnumerator AfterSqueeze()
    {
        canHide = false;
        yield return new WaitForSeconds (1f);
        canHide = true;
    }



    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds (rechargeWait);

        while(stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds (0.1f);
        }

       if (stamina > maxStamina)
        {
            stamina = maxStamina;
            staminaBar.fillAmount = stamina/maxStamina;
        }
    }


}
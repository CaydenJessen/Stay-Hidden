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
    public bool canJump = true; 
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
       
        if(Input.GetKey(KeyCode.LeftControl))
        {    
            Player.transform.localScale = new Vector3(1.862f, 0.3f , 1.862f);

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
        
        if(Input.GetKeyUp(KeyCode.LeftControl) && isFacingRight == false || stamina == 0 && isFacingRight == false)
        {
            pH.isHidden = false;
            Player.transform.localScale = new Vector3(1.862f, 1.862f , 1.862f);


        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && isFacingRight == true || stamina == 0 && isFacingRight == true)
        {
            pH.isHidden = false;
            Player.transform.localScale = new Vector3(-1.862f, 1.862f , 1.862f);

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
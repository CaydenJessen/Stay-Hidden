using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    public float health;
    public float maxHealth = 10f;
    public float healthRegeneration = 1f; //Amount of health gained
    

    public float healthRegenCooldown = 2f; //Time between health gain
    float lastHealTime;
    public float damageCooldown = 2f;
    float lastDamageTime;


    public bool inLight = false;

    public bool isHidden = false;
    public LineOfSight LOS;
    public EnemyAI ea;

    public Image healthBar;

    public GameObject playerSprite;

    public GameObject flashRed;

    public bool isAlive = true;

    public bool inDarkness = false;

    public GameObject alertIcon;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;

        
    }


    void FixedUpdate()
    {
        if(inDarkness == true)
        {
            isHidden = true;
        }

        Spotted();

        if (health <= 0)
        {
            isAlive = false;
            flashRed.SetActive(true);
            playerSprite.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(Death());
        }
        else
        {
            isAlive = true;
        }



    }

    public void TakeDamage (float amount)
    {

        if (isHidden == false) //Player can't take damage while being hidden
        {
            if (Time.time - lastDamageTime < damageCooldown && health>0) return;

            health -= amount;
            StartCoroutine(Hurt());
            lastDamageTime = Time.time;

            if (health <= 0)
            {
                isAlive = false;
                flashRed.SetActive(true);
                playerSprite.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(Death());
            }
            else
            {
                isAlive = true;
            }

        }
    
    }


    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (Time.time - lastHealTime < healthRegenCooldown) return;

        if (collision.gameObject.CompareTag("Darkness"))
        {
            isHidden = true;
            inDarkness = true;

            Healing();

            lastHealTime = Time.time;

            LOS.isChasing = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision) 
    {

        if (collision.gameObject.CompareTag("Darkness"))
        {
            isHidden = false;
            inDarkness = false;
        }
    }


    public void Healing ()
    {
        if (health < maxHealth)
        {
            health += healthRegeneration;
        }
    }
    IEnumerator Death()
    {
 
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game_Lost");
    }

    IEnumerator Hurt()
    {
        flashRed.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        flashRed.SetActive(false);
    }

    public void Spotted()
    {
        if((inLight == true)  && isHidden == false && inDarkness == false)
        {
            alertIcon.SetActive(true);
        }
        else
        {
            alertIcon.SetActive(false);
        }
    }



    //----------------TEMPORARY FIX AS EnemyDamage.cs IS NOT WORKING---------------////
    //----------------DELETE THIS FIX IF IT IS EnemyDamage.cs IS FIXED---------------////
    public float damage = 1f;
    public float attackCooldown = 2f;

    float lastAttackTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if we took damage recently
        if (Time.time - lastAttackTime < attackCooldown) return;


        if(collision.gameObject.CompareTag("Enemy")) 
        {
            if (isHidden == false)
            {
                TakeDamage(damage);

                // Note when the player took damage
                lastAttackTime = Time.time;
            }

        }
    }

//----------------END OF FIX---------------////

}

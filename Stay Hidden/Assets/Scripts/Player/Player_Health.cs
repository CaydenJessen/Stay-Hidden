using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public EnemyDamage eD;
    public float health;
    public float maxHealth = 10f;
    public float healthRegeneration = 1f; //Amount of health gained
    public Player_Movement pM;

    public float healthRegenCooldown = 2f; //Time between health gain
    float lastHealTime;
    public float damageCooldown = 2f;
    public float lastHealth;
    float lastDamageTime;


    public bool inLight = false;

    public bool isHidden = false;
    //public LineOfSight LOS;

    public Image healthBar;

    public GameObject playerSprite;

    public GameObject flashRed;

    public bool isAlive = true;

    public bool inDarkness = false;

    public GameObject alertIcon;
    public GameObject pupil;
    public Transform enemy;

    public float lightDamage = 2f;
    public float lightCooldown = 1f;
    float lastLightTime;

    public string sceneName;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        lastHealth = health;
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        
        if(lastHealth > health)
        {
            StartCoroutine(Hurt());
        }
    }


    void FixedUpdate()
    {
        if(inDarkness == true)
        {
            isHidden = true;
        }

        //Spotted Icon above Players head
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


    public float Damage = 1f;
    public float attackCooldown = 2f;

    float lastAttackTime;
    public bool HITCOLLIDER = false;

    private void OnTriggerStay2D(Collider2D collision) 
    {
        //Darkness Regeneration Timer//
        if (Time.time - lastHealTime < healthRegenCooldown) return;

        if (collision.gameObject.CompareTag("Darkness"))
        {
            isHidden = true;
            inDarkness = true;

            if (health < maxHealth)
            {
                health += healthRegeneration;
            }

            lastHealTime = Time.time;
        }

        if (collision.gameObject.CompareTag("Light"))
        {
            inLight = true;
        }
        
        //Death Pit//
        if (collision.gameObject.CompareTag("Death"))
        {
            health = 0;
        }

        //Damaging Light//
        if (Time.time - lastLightTime < lightCooldown) return;

        if (collision.gameObject.CompareTag("DamageLight"))
        {
            if(isHidden == false) // Player doesn't take damage if they are hiding
            {
                health = health - lightDamage;

                lastLightTime = Time.time;
            }
        }


        //TEMPORARY FIX FOR GRANNYS DAMAGE//////////////////////////////////////////////////////////////////////////
        if (Time.time - lastAttackTime < attackCooldown) return;

        if(collision.gameObject.CompareTag("Granny Collider")) 
        {
            HITCOLLIDER = true;
            if (isHidden == false)
            {
                Damage = eD.damage;
                TakeDamage(Damage);

                lastAttackTime = Time.time;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {

        if (collision.gameObject.CompareTag("Darkness"))
        {
            isHidden = false;
            inDarkness = false;
        }

        if (collision.gameObject.CompareTag("Light"))
        {
            inLight = false;
        }
    }




        public float knifeDamage = 2f;
        public float knifeCooldown = 2f;

        float lastKnifeTime;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (Time.time - lastKnifeTime < knifeCooldown) return;

            if(collision.gameObject.CompareTag("Damaging Environment")) 
            {
                if (isHidden == false)
                {
                    TakeDamage(knifeDamage);

                    lastKnifeTime = Time.time;
                }
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
         lastHealth = health;
    }

    public void Spotted()
    {   
        if(pM.isFacingRight == true)
            {
                pupil.transform.localScale = new Vector3(1, 1, 1);
            }
        else
        {
            pupil.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (/*inLight == true && isHidden == false && inDarkness == false*/sceneName == "Level_5_Basement")
        {
            alertIcon.SetActive(true);
            Vector3 difference = enemy.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            pupil.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
           
        }


        else
        {
            alertIcon.SetActive(false);
        }
    }







    

    // ----------------TEMPORARY FIX AS EnemyDamage.cs IS NOT WORKING---------------////
    // ----------------DELETE THIS FIX IF IT IS EnemyDamage.cs IS FIXED---------------////
    // public float Damage = 1f;
    // public float attackCooldown = 2f;

    // float lastAttackTime;
    // public bool HITCOLLIDER = false;

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     Check if we took damage recently
    //     if (Time.time - lastAttackTime < attackCooldown) return;


    //     if(collision.gameObject.CompareTag("Granny Collider")) 
    //     {
    //         HITCOLLIDER = true;
    //         if (isHidden == false)
    //         {
    //             Damage = eD.damage;
    //             TakeDamage(Damage);

    //             Note when the player took damage
    //             lastAttackTime = Time.time;
    //         }

    //     }
    // }

//----------------END OF FIX---------------////

}

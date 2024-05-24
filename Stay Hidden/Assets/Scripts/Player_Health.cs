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

        if (collision.gameObject.CompareTag("Light"))
        {
            inLight = true;
            LOS.isChasing = true;
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
        if(((inLight == true) || (LOS.isChasing == true)) && isHidden == false && inDarkness == false)
        {
            alertIcon.SetActive(true);
        }
        else
        {
            alertIcon.SetActive(false);
        }




    }
}

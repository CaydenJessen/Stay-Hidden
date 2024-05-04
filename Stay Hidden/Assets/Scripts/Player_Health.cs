using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{

    public float health;
    public float maxHealth = 10f;
    public float healthRegeneration = 1f; //Amount of health gained

    public float healthRegenCooldown = 2f; //Time between health gain
    float lastHealTime;

    public bool inLight = false;

    public bool isHidden = false;
    public LineOfSight LOS;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage (float amount)
    {

        if (isHidden == false) //Player can't take damage while being hidden
        {
            health -= amount;

            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }
    
    }


    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (Time.time - lastHealTime < healthRegenCooldown) return;

        if (collision.gameObject.CompareTag("Darkness"))
        {
            isHidden = true;

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

}

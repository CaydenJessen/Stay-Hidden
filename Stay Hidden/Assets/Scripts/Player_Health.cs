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

    public bool isDark = false;

    public bool isHidden;

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

        if (isDark == false) //Player can't take damage while being hidden
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
            isDark = true;
            isHidden = true;

            Healing();

            lastHealTime = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {

        if (collision.gameObject.CompareTag("Darkness"))
        {
            isDark = false;
            isHidden = false;
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

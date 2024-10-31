using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyLights : MonoBehaviour
{
    public GrannyAI gAI;
    public Player_Health playerHealth;

    public bool ligthCollide = false;


    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gAI.lightAlert = true;
            ligthCollide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gAI.lightAlert = false;
            ligthCollide = false;
        }
    }
}
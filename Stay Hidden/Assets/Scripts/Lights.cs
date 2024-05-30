using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public EnemyAI eA;
    public Player_Health playerHealth;
    // //public LineOfSight los;

    public bool ligthCollide = false;


    private void Update()
    {
        // if(playerHealth.inLight == true)
        // {
        //     eA.chase = true;
        // }
        // else
        // {
        //     eA.chase = false;
        //     //StartCoroutine(Wait());
        // }
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eA.chase = true;
            ligthCollide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eA.chase = false;
            ligthCollide = false;
        }
    }

   /* IEnumerator Wait()
    {
        eA.speed = 0f;
        yield return new WaitForSeconds(3);
        eA.speed = eA.walkSpeed;
    }*/







}

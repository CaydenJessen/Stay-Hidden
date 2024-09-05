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

    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eA.lightAlert = true;
            ligthCollide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eA.lightAlert = false;
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

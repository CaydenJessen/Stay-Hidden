using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public EnemyAI eA;
    public Player_Health playerHealth;
    public LineOfSight los;

    private void Update()
    {
        if(playerHealth.inLight == true)
        {
            eA.chase = true;
        }
        else
        {
            eA.chase = false;
            //StartCoroutine(Wait());
        }
    }
    
   /* IEnumerator Wait()
    {
        eA.speed = 0f;
        yield return new WaitForSeconds(3);
        eA.speed = eA.walkSpeed;
    }*/
}

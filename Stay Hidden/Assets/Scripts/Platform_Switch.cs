using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Switch : MonoBehaviour
{
   
    public bool inRange = false;
    public bool switchOn = false;
    public GameObject lever;
    public GameObject leverControl;
    public bool status = false;
    public Moving_Platform MovPlat;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if ((inRange == true) && (switchOn == false))
            {
                switchOn = true;
                MovPlat.platIsMove = true;
                status = true; 
                leverControl.transform.Rotate(0.0f, 0.0f, -41.0f);
            }
            else
            {
                if ((inRange == true) && (switchOn == true))
                {
                    switchOn = false;
                    MovPlat.platIsMove = false;
                    status = false; 
                    leverControl.transform.Rotate(0.0f, 0.0f, 41.0f);

                }
            }

        }
        if (inRange == true)
        {
            lever.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            lever.GetComponent<Renderer>().material.color = Color.white;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}

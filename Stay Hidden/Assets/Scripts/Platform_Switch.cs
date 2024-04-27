using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Switch : MonoBehaviour
{
   
    public bool inRange = false;
    public bool switchOn = false;

    public Moving_Platform MovPlat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if ((inRange == true) && (switchOn == false))
            {
                switchOn = true;
                MovPlat.platIsMove = true;
            }
            else
            {
                if ((inRange == true) && (switchOn == true))
                {
                    switchOn = false;
                    MovPlat.platIsMove = false;
                }
            }
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

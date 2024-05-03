using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject[] openMechanisms;
    public GameObject[] closeMechanisms;
    public GameObject lever;
    public GameObject leverBase;
    public bool inRange = false;
    public bool status = false;
    public int numberOfMechanisms;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if ((inRange == true) && (status == false))
            {
                for (int i = 0; i < numberOfMechanisms; i++)
                {
                    openMechanisms[i].SetActive(false);
                    closeMechanisms[i].SetActive(true);
                }
                
                leverBase.transform.Rotate(0.0f, 0.0f, 78.0f);

                status = true;
            }
            else if ((inRange == true) && (status == true))
                {
                for (int i = 0; i < numberOfMechanisms; i++)
                {
                    openMechanisms[i].SetActive(true);
                    closeMechanisms[i].SetActive(false);
                }
                leverBase.transform.Rotate(0.0f, 0.0f, -78.0f);
                    status = false;
                }
         
        }

        if(inRange == true)
        {
            lever.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            lever.GetComponent<Renderer>().material.color = Color.white;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inRange = true;          
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = false;
        }

    }
}

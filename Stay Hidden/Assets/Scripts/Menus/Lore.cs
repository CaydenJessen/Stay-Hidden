using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lore : MonoBehaviour
{
    public bool inRange = false;
    public bool reading = false;
    public GameObject paper;
    public GameObject interactable;
    public Player_Movement pM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.E))
         {
            if ((inRange == true) && (reading == false))
            {
                 paper.SetActive(true);
                 Time.timeScale = 0f;
            }
         }
         else
         {
             if ((inRange == true) && (reading == true))
             {
                 paper.SetActive(false);
                 Time.timeScale = 1f;
             }
            
         }

         if(inRange == true)
         {
            pM.viewing = true;
         }
         else
         {
            pM.viewing = false;
         }
    }

    private void OnTriggerStay2D(Collider2D paper) 
    {
         if (paper.gameObject.CompareTag("Player"))
        {
            inRange = true;
            //interactable.GetComponent<Renderer>().material.color = Color.blue;

        }

    }

     private void OnTriggerExit2D(Collider2D paper)
    {
        if (paper.gameObject.CompareTag("Player"))
        {
            inRange = false;
            //interactable.GetComponent<Renderer>().material.color = Color.white;
        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Switch : MonoBehaviour
{
    public GameObject[] Light;
    public bool inRange = false;
    public bool switchOn = true;
    public GameObject lightSwitch;
    public int numberOfMechanisms;

    //public AlternatingLights AL;
    public Player_Movement pM;

    // Start is called before the first frame update
    void Start()
    {
        numberOfMechanisms--;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if ((inRange == true) && (switchOn == false))
            {
                for (int i = 0; i < Light.Length; i++)
                {
                    Light[i].SetActive(true);
                    switchOn = true;
                    //AL.lightSetup = false;
                }
            }
            else
            {
                if ((inRange == true) && (switchOn == true))
                {
                    for (int i = 0; i < Light.Length; i++)
                    {
                        Light[i].SetActive(false);
                        switchOn = false;
                        //AL.lightSetup = false;
                    }
                }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            lightSwitch.GetComponent<Renderer>().material.color = Color.green;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            lightSwitch.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}











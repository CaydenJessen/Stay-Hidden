using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Switch : MonoBehaviour
{
    public GameObject Light;
    public bool inRange = false;
    public bool switchOn = false;

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
                Light.SetActive(false);
                switchOn = true;
            }
            else
            {
                if ((inRange == true) && (switchOn == true))
                {
                    Light.SetActive(true);
                    switchOn = false;
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

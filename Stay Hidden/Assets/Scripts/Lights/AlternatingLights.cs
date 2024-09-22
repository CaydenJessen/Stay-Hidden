using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingLights : MonoBehaviour
{
    public GameObject[] Lights;
    
    public float lightCooldown = 2f; //Time between lights on and off.

    public bool lightOn = true;

    public bool lightSetup = true;

    public Light_Switch LS;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    { 
        if(LS.switchOn == true)
        {
            lightSetup = false;
        }

        if (lightSetup == false)
        {
            foreach (GameObject i in Lights)
            {
                i.SetActive(false);
            }
        }
    }

    IEnumerator ExampleCoroutine()
    {
        while (lightSetup == true)
        {
            if (lightSetup == true)
            {
                foreach (GameObject i in Lights)
                {
                    if (lightOn == false)
                    {
                        i.SetActive(false);
                        lightOn = true;
                        yield return new WaitForSeconds(lightCooldown);
                    }

                    if (lightOn == true)
                    {
                        i.SetActive(true);
                        lightOn = false;
                        yield return new WaitForSeconds(lightCooldown);
                    }
                }
            }
        }
    }
}

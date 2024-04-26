using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public GameObject Light;

    public float lightCooldown = 2f; //Time between health gain
    float lastLight;

    public bool lightOn = false;

    public bool lightSetup = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ExampleCoroutine()
    {
        while (lightSetup == true)
        {
            if (lightOn == false)
            {
                Light.SetActive(false);
                yield return new WaitForSeconds(lightCooldown);
                lightOn = true;
            }

            if (lightOn == true)
            {
                Light.SetActive(true);
                yield return new WaitForSeconds(lightCooldown);
                lightOn = false;

            }
        }

    }


}

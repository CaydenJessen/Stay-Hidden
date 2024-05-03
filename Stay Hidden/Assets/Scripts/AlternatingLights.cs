using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingLights : MonoBehaviour
{
    public GameObject[] Light;

    public Player_Health playerHealth;
    public LineOfSight los;


    public float lightCooldown = 2f; //Time between lights on and off.
    float lastLight;

    public bool lightOn = true;

    public bool lightSetup = true;

    public bool isAlternating;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerHealth.inLight == true)
        //{
        //    los.isChasing = true;
        //}
    }

    IEnumerator ExampleCoroutine()
    {
        for (int i = 0; i < Light.Length; i++)
        {
            while (lightSetup == true)
            {
                if (lightOn == false)
                {
                    Light[i].SetActive(false);
                    yield return new WaitForSeconds(lightCooldown);
                    lightOn = true;
                }

                if (lightOn == true)
                {
                    Light[i].SetActive(true);
                    yield return new WaitForSeconds(lightCooldown);
                    lightOn = false;

                }
            }
        }

    }


}

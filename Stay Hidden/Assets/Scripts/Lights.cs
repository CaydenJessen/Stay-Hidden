using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public GameObject Light;

    public Player_Health playerHealth;
    public LineOfSight los;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.inLight == true)
        {
            //Make a subroutine here so enemy doesnt immediately idle??
            los.isChasing = true;
        }
        else
        {
            los.isChasing = false;
        }
    }
}

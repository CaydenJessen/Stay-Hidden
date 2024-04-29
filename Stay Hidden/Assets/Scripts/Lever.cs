using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject mechanism;
    public GameObject lever;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            mechanism.SetActive(false);
            lever.GetComponent<Renderer>().material.color = Color.green;

        }
    }
}

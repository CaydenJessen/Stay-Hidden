using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float speed = 2f;
    public float height = 0.2f;


    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height + transform.position.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

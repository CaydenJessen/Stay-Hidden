using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour
{
    public float amp;
    public float freq;
    Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
        transform.position = initPos;
    }

    private void Update()
    {
        
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, 0);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail_Anim : MonoBehaviour
{
    public Animator animate;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animate.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

    }
}

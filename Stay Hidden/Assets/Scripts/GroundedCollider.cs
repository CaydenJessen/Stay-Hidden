using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCollider : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if((other.tag == "Ground") || (other.tag == "Moving Platform"))
        {
            anim.SetBool("Grounded", true);
        }
        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if((other.tag == "Ground") || (other.tag == "Moving Platform"))
        {
            anim.SetBool("Grounded", false);
        }
    }
}

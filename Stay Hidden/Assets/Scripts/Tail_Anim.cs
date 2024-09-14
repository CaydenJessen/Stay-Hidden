using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail_Anim : MonoBehaviour
{
    public Animator animate;    
    Vector3 lastMouseCoordinate = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;

            // Then we check if it has moved to the left.
        if (mouseDelta.x < 0 || mouseDelta.y >0) 
        {
            animate.SetBool("curveUp", false);
        }
        else if(mouseDelta.x > 0 || mouseDelta.y < 0)
        {
            animate.SetBool("curveUp", true);
        }
        else if(mouseDelta.x == 0 || mouseDelta.y == 0)
        {
            animate.SetBool("Straight", true);
        }



        lastMouseCoordinate = Input.mousePosition;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail_Animation : MonoBehaviour
{
    public Animator animate;
    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
    Debug.Log(Input.mousePosition - lastMouseCoordinate);
    if(mouseDelta.x < -0.1 || mouseDelta.y > 0.1)
    {
        animate.SetBool("CurveRight", false);
    }
    else if(mouseDelta.x > 0.1 || mouseDelta.y < -0.1)
    {
        animate.SetBool("CurveRight", true);
    }
    lastMouseCoordinate = Input.mousePosition;
    }
}

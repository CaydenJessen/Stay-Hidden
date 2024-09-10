using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailMechanic : MonoBehaviour
{
    public GameObject tail;
    public Player_Movement pM;
    public Camera mainCam;
    private float cameraZDistance;
    public Transform tailParent;
    public Vector3 initialScale;
    Vector3 lastMouseCoordinate = Vector3.zero;
    // public Animator animate;

    private void Start()
    {
        cameraZDistance = mainCam.WorldToScreenPoint(transform.position).z;
       // animate = GetComponent<Animator>();
    }
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            tail.SetActive(true);
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            Vector3 MouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
            Vector3 MouseWorldPosition = mainCam.ScreenToWorldPoint(MouseScreenPosition);
            float distance = Vector3.Distance(tailParent.position, MouseWorldPosition);
            transform.localScale = new Vector3(distance / 2f, initialScale.y, initialScale.z);

            if (pM.isFacingRight == false)
            {
                transform.localScale = new Vector3(distance / 2f, initialScale.y, initialScale.z);

            }
            else
            {
                transform.localScale = new Vector3(-distance / 2f, initialScale.y, initialScale.z);
            }

            if (transform.localScale.x > 1f)
            {
                transform.localScale = new Vector3(1, initialScale.y, initialScale.z);
            }
            else if(transform.localScale.x < -1)
            {
                transform.localScale = new Vector3(-1, initialScale.y, initialScale.z);
            }
        }
        else
        {
            tail.SetActive(false);
        }

        
  /*  Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
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
*/
    }

}

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

    private void Start()
    {
        cameraZDistance = mainCam.WorldToScreenPoint(transform.position).z;
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
            //tail.transform.localScale = new Vector3(Input.mousePosition.x + Input.mousePosition.y, 1f, 1f);
        }
        else
        {
            tail.SetActive(false);
        }
        //Flip();
    }

    /*void Flip()
    {
        if(pM.isFacingRight == false)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }*/

}

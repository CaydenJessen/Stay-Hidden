using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator door;
    public Lever lever;
    public GameObject DoorCollider;
    //public bool cutScene = false;
    //public Camera_Controller CC;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lever.status==true)
        {
            //StartCoroutine(Wait());          
            door.SetBool("DoorOpen", true);
            DoorCollider.SetActive(false);
            
        }
        else if(lever.status == false)
        {
            
            door.SetBool("DoorOpen", false);
            DoorCollider.SetActive(true);
        }
    }

    //private IEnumerator Wait()
    //{
    //    cutScene = true;
    //    yield return new WaitForSeconds(CC.cutLength);
    //    cutScene = false;
    //    //StopCoroutine(Wait());
    //}

}

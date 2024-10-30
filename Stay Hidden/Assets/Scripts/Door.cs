using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator door;
    public Lever lever;
    public GameObject DoorCollider;
    public DoorTrigger dT;
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
            door.SetBool("DoorOpen", true);
            DoorCollider.SetActive(false);
            dT.isDoorOpening = true;
        }
        else if(lever.status == false)
        {
            door.SetBool("DoorOpen", false);
            DoorCollider.SetActive(true);
        }
    }
}

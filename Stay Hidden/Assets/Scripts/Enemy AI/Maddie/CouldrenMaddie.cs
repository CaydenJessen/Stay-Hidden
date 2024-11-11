using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouldrenMaddie : MonoBehaviour
{
    public GameObject maddieTrigger;
    public DoorTrigger dT;

    // Start is called before the first frame update
    void Start()
    {
        maddieTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dT.doorOpen == true)
        {
            maddieTrigger.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaTrigger : MonoBehaviour
{
    public bool seePlayer = false;
    public bool checkConfuse = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            seePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            seePlayer = false;
        }
    }
}

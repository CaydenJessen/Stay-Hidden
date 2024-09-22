using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaddieTrigger : MonoBehaviour
{
    public MaddieAI madAI;
    public bool collide = false;
    public GameObject Maddie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            if(madAI.deSpawn == false)
            {
                madAI.chasePlayer = true;
                collide = true;
                Maddie.SetActive(true);
            }

        }
    }
}

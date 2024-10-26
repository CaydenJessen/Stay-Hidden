using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ParticleSystem Particles;
    public Deposit depo;
    public Player_Movement pM;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            Particles.Play();
            pM.hasItem = true;
            pM.num++;
            Destroy(gameObject);
        }

    }
}

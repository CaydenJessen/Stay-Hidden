using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public ParticleSystem Particles;
    public AudioClip breakSound; 
    private AudioSource audioSource;

 void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Tail")
        {
            audioSource.PlayOneShot(breakSound);
            Destroy(gameObject);
            Particles.Play();
            Debug.Log("destroyed");
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deposite : MonoBehaviour
{
    public ParticleSystem Particles;
    public GameObject[] item;
    public Transform[] depositePosition;
    public Player_Movement iTM;
    public Player_Movement number;
    public int deposited = 5;
    private int count = 0;

    private void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && iTM.hasItem == true && deposited>0 && number.num >= 0)
        {
            Particles.Play();
            Instantiate(item[count], depositePosition[count].position, depositePosition[count].rotation);
            deposited--;
            count++;
            number.num--;
        }
    }
}

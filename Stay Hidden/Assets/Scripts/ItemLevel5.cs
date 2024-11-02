using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevel5 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public Deposit depo;
    void Start()
    {
        item1.SetActive(true);
        item2.SetActive(false);
        item3.SetActive(false);
        item4.SetActive(false);
        item5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(item1 == null && depo.deposited == 4)
        {
            item2.SetActive(true);
        }
        if(item2 == null&& depo.deposited == 3)
        {
            item3.SetActive(true);
        }
        if(item3 == null&& depo.deposited == 2)
        {
            item4.SetActive(true);
        }
        if(item4 == null&& depo.deposited == 1)
        {
            item5.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyBox : MonoBehaviour
{
    public GrannyAI gAI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gAI.transformFive == true)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(2.580919f, 1.331993f);
            //GetComponent<BoxCollider2D>().offset = new Vector2(0.1062403f, -0.8568106f); //0.79
            GetComponent<BoxCollider2D>().offset = new Vector2(0.1062403f, 0.06681f); //0.79
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float alpha = 0;
    public float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alpha =  alpha + fadeSpeed * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
    }
}

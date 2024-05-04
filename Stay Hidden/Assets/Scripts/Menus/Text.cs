using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text : MonoBehaviour
{
    public GameObject[] text;
    public int numberOfTexts;
    public int waitSeconds;



    // Start is called before the first frame update
    void Start()
    {
            text[0].SetActive(true);
    }

    IEnumerator textScroll()
    {
        yield return new WaitForSeconds(waitSeconds);
        for (int i = 0; i < numberOfTexts; i++)
        {
            text[i].SetActive(true);
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        StartCoroutine(textScroll());
    }

}

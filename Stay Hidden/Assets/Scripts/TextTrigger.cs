using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    public GameObject text;
    //public bool dialogue;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D col)
    {
      if (col.gameObject.CompareTag("Player"))
      {
            //dialogue = true;
            text.SetActive(true);
      }
    }

    private void OnTriggerExit2D(Collider2D colli)
    {
        if (colli.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }



}

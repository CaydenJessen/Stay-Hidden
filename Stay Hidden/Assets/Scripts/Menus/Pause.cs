using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused; 
    // Start is called before the first frame update
   /* void Click()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            isPaused = true; 
        }
   }
*/ 
    // Update is called once per frame
     void Update()
    {
        

         if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("hello");
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true; 
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false; 
        }
        
    }
}

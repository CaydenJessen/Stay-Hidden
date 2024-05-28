using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string sceneName;
    public GameObject pauseMenu;
    public GameObject[] objects;
    public bool on = true;
  
    public void LoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);   
        Debug.Log("hey i reset");
    }


    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void turnoff()
    {
        if(on == true)
        {
            for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].SetActive(false);
                    on = false;
                }
            
        }
        else
        {
            for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].SetActive(true);
                    on = true;
                }
        }


    }
        
    

}


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
  
    // changed loadscene bit with coroutine
    public void LoadScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneWithDelay());
    }

    // coroutine adds dealy
    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(2f); // Delay for 3 seconds
        SceneManager.LoadScene(sceneName);   
        //Debug.Log("hey i reset");
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

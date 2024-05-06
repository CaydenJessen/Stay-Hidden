using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string sceneName;
    public GameObject pauseMenu;
  
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

}


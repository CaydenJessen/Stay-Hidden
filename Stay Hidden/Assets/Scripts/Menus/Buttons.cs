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
    public float clickDelay;
 
    public void LoadScene()
    {
        StartCoroutine(Load());
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

                    if(gameObject.tag == "Paper")
                    {
                        Time.timeScale = 1f;
                    }
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
       
   private IEnumerator Load()
   {
    yield return new WaitForSeconds (clickDelay);
    Time.timeScale = 1f;
    SceneManager.LoadScene(sceneName); 
   }

   public void ExitGame()
   {
    Application.Quit();
   }


}

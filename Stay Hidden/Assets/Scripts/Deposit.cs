using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deposit : MonoBehaviour
{
    public ParticleSystem Particles;
    public GameObject[] item;
    public Transform[] depositePosition;
    public Player_Movement PM;
    public int deposited = 5;
    public int count = 0;
    public bool inRange = false;
    public GameObject nextLevel;
    public int waitTime;

    //private string[] levelNames = { "Level_1_Attic", "Level_2_GreenRm", "Level_3_Kitchen", "Level_5_Basement" };

    private void Update()
    {
        if(deposited == 0)
        {
            StartCoroutine(Victory());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inRange == true)
            {
                if (PM.hasItem == true && deposited > 0 && PM.num >= 0)
                {
                    Particles.Play();
                    Instantiate(item[count], depositePosition[count].position, depositePosition[count].rotation);
                    deposited--;
                    count++;
                    PM.num--;
                }
            }

        }

        if (inRange == true)
        {
            Particles.Play();
        }
        else
        {
            Particles.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = false;
        }

    }
    IEnumerator Victory()
    {
        yield return new WaitForSeconds(waitTime);

        if (SceneManager.GetActiveScene().name == "Level_5_Basement")
        {
            SceneManager.LoadScene("Game_Won");
        }
        else
        {
        nextLevel.SetActive(true);
        PM.currentSpeed = 0f;

        //string currentSceneName = SceneManager.GetActiveScene().name;

       
        //int currentIndex = System.Array.IndexOf(levelNames, currentSceneName);

        //if (currentIndex >= 0 && currentIndex < levelNames.Length - 1)
        //{
            
        //    string nextSceneName = levelNames[currentIndex + 1];
        //    SceneManager.LoadScene(nextSceneName);
        //}
        //else
        //{
        //SceneManager.LoadScene("Game_Won");
        //}
        }
    }   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deposit : MonoBehaviour
{
    public ParticleSystem Particles;
    public GameObject[] item;
    public Transform[] depositePosition;
    public Player_Movement iTM;
    public Player_Movement number;
    public int deposited = 5;
    private int count = 0;
    public bool inRange = false;

    private string[] levelNames = { "Level_1_Attic", "Level_2_GreenRm", "Level_3_Kitchen", "Level_5_Basement" };

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
                if (iTM.hasItem == true && deposited > 0 && number.num >= 0)
                {
                    Particles.Play();
                    Instantiate(item[count], depositePosition[count].position, depositePosition[count].rotation);
                    deposited--;
                    count++;
                    number.num--;
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
        yield return new WaitForSeconds(3);

        string currentSceneName = SceneManager.GetActiveScene().name;

       
        int currentIndex = System.Array.IndexOf(levelNames, currentSceneName);

        if (currentIndex >= 0 && currentIndex < levelNames.Length - 1)
        {
            
            string nextSceneName = levelNames[currentIndex + 1];
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
        SceneManager.LoadScene("Game_Won");
        }
    }   
}

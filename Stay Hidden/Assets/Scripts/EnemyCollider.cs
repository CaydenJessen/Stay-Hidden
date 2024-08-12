using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{

    public Player_Health pH;
    public GameObject enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(pH.isHidden == false)
        {
            enemyCollider.SetActive(true);
        }
        else
        {
            enemyCollider.SetActive(false);
        }
    }
}

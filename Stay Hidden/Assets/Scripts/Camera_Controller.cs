using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public  GameObject player;

    //Camera variables with offset
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    public float yOffset; //Camera Height

    // Update is called once per frame
    void Update()
    {

        //Simple Camera Movement with no offset:
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        
        
        //Camera movement with offset
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    
        if(player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y + yOffset, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y + yOffset, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
        
    }
}

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
    public float cameraSize;
    public Camera cam;
    public Player_Health pH;
    public Player_Movement pM;
    public float zoomOut;

    public float yOffset; //Camera Height
    private void Start()
    {
        cam.orthographicSize = 5f;
    }
    // Update is called once per frame
    void Update()
    {

        if (pH.isAlive == true)
        {
        //Simple Camera Movement with no offset:
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        if(pH.isHidden == true)
        {
            cam.orthographicSize = 3f;
        }
        else if(pM.camResize == true)
        {
            cam.orthographicSize = zoomOut;
        }
        else
        {
            cam.orthographicSize = 5f;

        }



        //Camera movement with offset
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    
        if(player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y + 2 + yOffset, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y + 2 + yOffset, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);

        }
       
        
    }
}

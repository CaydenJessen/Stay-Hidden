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
    public Camera cam;
    public Player_Health pH;
    public Player_Movement pM;
    //public Door door;
    public float zoomOut;
    public float zoomIn;
    public float zoomSpeed = 4f;
    public float zoomDown = 4;
    public float camSize = 5f;

    public float yOffset; //Camera Height

    public GameObject cutDoor;
    private Vector3 doorPosition;
    public float cutDoorSpeed;
    public DoorTrigger dT;
    public bool CAMERAMOOOVE = false;
    public float cutLength = 2f;

    private void Start()
    {
        cam.orthographicSize = 5f;
    }
    // Update is called once per frame
    void Update()
    {

        if ((pH.isAlive == true) && (dT.isDoorOpening == false))
        {
            //Simple Camera Movement with no offset:
            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            if (pH.isHidden == true)
            {

                if (cam.orthographicSize >= zoomIn + 0.1f)
                {
                    cam.orthographicSize -= zoomSpeed * Time.deltaTime;
                }
                else
                {
                    cam.orthographicSize = zoomIn;
                }

            }
            else if (pM.camResize == true)
            {

                if (cam.orthographicSize <= zoomOut - 0.1f)
                {
                    cam.orthographicSize += zoomSpeed * Time.deltaTime;
                }
                else
                {
                    cam.orthographicSize = zoomOut;
                }
            }
            else
            {
                if (cam.orthographicSize > camSize + 0.1f)
                {
                    cam.orthographicSize -= zoomSpeed * Time.deltaTime;
                }
                else if (cam.orthographicSize < camSize - 0.1f)
                {
                    cam.orthographicSize += zoomSpeed * Time.deltaTime;
                }
                else
                {
                    cam.orthographicSize = camSize;

                }


            }
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

            if ((Input.GetKey(KeyCode.S) && (pM.canCrouch == true)))
            {
                playerPosition = new Vector3(playerPosition.x, playerPosition.y - zoomDown, -10);
            }

            CamFollow();
            //Camera movement with offset


            if (dT.isDoorOpening /*|| door.cutScene == true*/)
            {
                DoorCutscene();

            }



        }
        
        
    }

    private void CamFollow()
    {
        
        if ((player.transform.localScale.x > 0f && Input.GetKey(KeyCode.S) == false) && pM.canCrouch == true)
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y + 2 + yOffset, playerPosition.z);
        }
        else if (player.transform.localScale.x < 0f && Input.GetKey(KeyCode.S) == false)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y + 2 + yOffset, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }

    private void DoorCutscene()
    {
            CAMERAMOOOVE = true;
            doorPosition = new Vector3(cutDoor.transform.position.x, cutDoor.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, doorPosition, cutDoorSpeed * Time.deltaTime);

    }


}

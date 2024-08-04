using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform groundDetection;

    [SerializeField] Transform wallDetection;
    [SerializeField] LayerMask wallLayerMask;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            // if (movingRight == true)
            // {
            //     transform.eulerAngles = new Vector3(0, -180, 0);
            //     movingRight = false;
            // }
            // else
            // {
            //     transform.eulerAngles = new Vector3(0, 0, 0);
            //     movingRight = true;
            // }
          if( movingRight==true )
            {
                Vector3 origin = wallDetection.position;
                Vector3 dir = Vector2.right;
                RaycastHit2D hit = Physics2D.Raycast( origin , dir , distance , wallLayerMask );
                if( hit.collider!=null )
                {
                    movingRight = false;
                    Debug.DrawLine( origin , hit.point , Color.red );
                }
                else Debug.DrawLine( origin , origin + dir*distance , Color.white , 0.01f );
            }
            else
            {
                Vector3 origin = wallDetection.position;
                Vector3 dir = -Vector2.right;
                RaycastHit2D hit = Physics2D.Raycast( origin , dir , distance , wallLayerMask );
                if( hit.collider!=null )
                {
                    movingRight = true;
                    Debug.DrawLine( origin , hit.point , Color.red );
                }
                else Debug.DrawLine( origin , origin + dir*distance , Color.white , 0.01f );
            }
        }
        
    }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("E");
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }

            }
        }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{

    public Transform posA, posB;
    public float platSpeed = 2f;
    Vector2 targetPos;
    public bool platIsMove = false;
    public float targetSize = 0.5f;

    void Start()
    {
       targetPos = posB.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (Vector2.Distance(transform.position, posA.position) < 0.1f) 
        {
            targetPos = posB.position;
        }

        if (Vector2.Distance(transform.position, posB.position) < 0.1f) 
        {
            targetPos = posA.position;
        }


        if(platIsMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, platSpeed * Time.deltaTime);

        }
    }

/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posA.transform.position, targetSize);
        Gizmos.DrawWireSphere(posB.transform.position, targetSize);
        Gizmos.DrawLine(posA.transform.position, posB.transform.position);
    }
    /*
        public float speed;
        public int startPoint;
        public Transform[] points;

        private int i;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = points[startPoint].position;
        }


        // Update is called once per frame
        void Update()
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
            {
                i++;

                if (i == points.Length)
                {
                    i = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.transform.SetParent(transform);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            collision.transform.SetParent(null);
        }


        */

}

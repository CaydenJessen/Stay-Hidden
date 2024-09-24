using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingKnife : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    public float distance;
    bool isFalling = false;
    bool isRising = false;
    Vector2 P;
    public float shakeAmount = 1.0f; 
    Vector2 startPosition;

        
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        startPosition.x = transform.position.x;
        startPosition.y = transform.position.y;
        P = transform.position;

    }

    private void Update()
    {
        if(isRising == true)
        { 
            Rising();
            isRising = false;
        }
        Physics2D.queriesStartInColliders = false;
        if (isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Player")
                {
                    StartCoroutine(Falling());
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
     {

        isRising = true;

    }
    

    IEnumerator Falling()
    {
        transform.position = P;
        for (int i = 0; i < 10; i++)
        {
            P.x = startPosition.x + shakeAmount;
            yield return new WaitForSeconds(0.1f);
            P.x = startPosition.x - shakeAmount;
            P.y = startPosition.y + shakeAmount;
            yield return new WaitForSeconds(0.1f);
            P.y = startPosition.y - shakeAmount;

        }
        rb.gravityScale = 5;
        isFalling = true;
    }

    IEnumerator Rising()
    {
        yield return new WaitForSeconds(2);
        transform.position = Vector3.MoveTowards(transform.localPosition, startPosition, 1);
        rb.gravityScale = 0;
        isFalling = false;
    }
}

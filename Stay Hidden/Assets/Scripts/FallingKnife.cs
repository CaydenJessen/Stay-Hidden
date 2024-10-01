using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingKnife : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    public float distance;
    public bool isFalling = false;
    public bool isRising = false;
    Vector2 P;
    public float shakeAmount = 1.0f; 
    Vector2 startPosition;
    public Transform target;
    public float fallTime;

    public float riseSpeed;
        
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
        if(isFalling == true)
        {
            StartCoroutine(Falling());
        }
        if(isRising == true)
        { 
           StartCoroutine(Rising());
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
                    isFalling = true;
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
        for (int i = 0; i < fallTime; i++)
        {
            P.x = startPosition.x + shakeAmount;
            yield return new WaitForSeconds(0.1f);
            P.x = startPosition.x - shakeAmount;
            P.y = startPosition.y + shakeAmount;
            yield return new WaitForSeconds(0.1f);
            P.y = startPosition.y - shakeAmount;

        }
        rb.gravityScale = 5;
        isFalling = false;
    }

    IEnumerator Rising()
    {
        yield return new WaitForSeconds(2);
        float step = riseSpeed * Time.deltaTime;
        rb.gravityScale = 0;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);  
        isRising = false;
    }
}

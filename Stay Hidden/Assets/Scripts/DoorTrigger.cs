using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Color activatedColor = Color.red;
    public GameObject door;
    public float doorOpenSpeed = 3f;

    private bool isDoorOpening = false;
    private Color originalColor;
    private Renderer itemRenderer;

    private void Start()
    {
        itemRenderer = GetComponent<Renderer>();
        originalColor = itemRenderer.material.color;
    }

    // Called when the box collides with the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            Debug.Log("Box has collided with the trigger!");
            itemRenderer.material.color = activatedColor;
            isDoorOpening = true;
        }
    }

    private void Update()
    {
        if (isDoorOpening)
        {
            door.transform.position += Vector3.up * doorOpenSpeed * Time.deltaTime;

            if (door.transform.position.y >= 5f)
            {
                isDoorOpening = false;
            }
        }
    }
}

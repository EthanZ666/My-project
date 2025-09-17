using System.Runtime.CompilerServices;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int value = 10;
    public float speed = 2f;
    private int direction = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        rb.linearVelocity = new Vector2(direction * speed, 0);

        if (sr != null)
            sr.flipX = (direction < 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            direction = -direction;
        }
    }
    
}

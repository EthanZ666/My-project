
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 35f;
    public float stopDistance = 50f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        float targetX = mousePos.x;
        float currentX = transform.position.x;
        float distance = Mathf.Abs(targetX - currentX);

        if (distance > stopDistance)
        {
            float directionX = Mathf.Sign(targetX - currentX);
            Vector2 newPosition = new Vector2(
                currentX + directionX * moveSpeed * Time.fixedDeltaTime,
                rb.position.y
            );

            rb.MovePosition(newPosition);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

    }
}

using UnityEngine;

public class Gauge : MonoBehaviour
{
    public GameObject gaugeObj; // "gauge" GameObject in Inspector
    public GameObject hookObj; // "hook" GameObject in Inspector
    public float moveDistance = 15f; // Total distance (from -5 to +5)
    public float moveSpeed = 9f;     // Base movement speed
    public float howfardown = -50f; // Default drop depth
    private bool willmove = true;
    private bool moving = false;
    private float gaugeValue = 0f; // -1 (left) to 1 (right)
    private Vector3 startPos = new Vector3(0f, -4.2704f, 0f);

    void Start()
    {
        willmove = true;
        if (gaugeObj != null)
        {
            gaugeObj.transform.localPosition = startPos;
            var rb = gaugeObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Kinematic; // Prevent falling
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }

    public void ShowGauge()
    {
        willmove = true;
    }

    void Update()
    {
        moving = hookObj.GetComponent<HookController>().movinghook;
        Debug.Log("Hook moving: " + moving);
        if (gaugeObj != null && willmove)
        {
            // Non-linear movement: faster in the middle, slower at the edges
            // Use a sine wave for smooth oscillation
            float t = Mathf.Sin(Time.time * moveSpeed); // t goes from -1 to 1
            float x = t * (moveDistance / 2f);

            gaugeObj.transform.localPosition = new Vector3(startPos.x + x, startPos.y, startPos.z);
            gaugeValue = x / (moveDistance / 2f); // -1 (left) to 1 (right)
        }

        if (willmove && Input.GetKeyDown(KeyCode.Space))
        {
            willmove = false;
            // The closer gaugeValue is to 0, the deeper the depth (closer to -100)
            float minDepth = -20f;
            float maxDepth = -100f;
            float mappedDepth = Mathf.Lerp(minDepth, maxDepth, 1f - Mathf.Abs(gaugeValue));
            howfardown = mappedDepth;
            Debug.Log($"Gauge Value: {gaugeValue}, Mapped Depth: {mappedDepth}");

            // Ensure gaugeObj stays at its last position and does not fall
            if (gaugeObj != null)
            {
                var rb = gaugeObj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.gravityScale = 0;
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (moving == false)
            {
                willmove = true;
            }
        }
    }
}
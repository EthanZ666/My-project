using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting; // needed for List

[RequireComponent(typeof(Rigidbody2D))]
public class HookController : MonoBehaviour
{
    public Transform hookTip;             // empty child at tip of hook
    public float dropDepth = -10f;        // how far down the hook goes
    public float dropSpeed = 10f;         // speed going down
    public float riseSpeed = 5f;          // speed going up
    public MoneyManager moneyManager;     // assign in Inspector

    public FishSpawner fishSpawner;
    public int maxCaughtFish = 10;         // adjustable in Inspector or by script

    private Rigidbody2D rb;
    private Vector3 startPos;

    private int money;
    private bool isDropping = false;
    private bool isRising = false;
    private List<Fish> caughtFishList = new List<Fish>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        // Drop hook
        if (Input.GetKeyDown(KeyCode.Space) && !isDropping && !isRising)
        {
            isDropping = true;
        }

        // Sell fish (only at start position & not dropping/rising)
        if (Input.GetKeyDown(KeyCode.S) && caughtFishList.Count > 0 && !isDropping && !isRising)
        {
            SellFish();
        }
    }

    void FixedUpdate()
    {
        if (isDropping)
        {
            // Go straight down
            rb.MovePosition(transform.position + Vector3.down * dropSpeed * Time.fixedDeltaTime);

            if (transform.position.y <= dropDepth)
            {
                isDropping = false;
                isRising = true;
            }
        }
        else if (isRising)
        {
            // Go up + horizontal movement with mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector3 target = new Vector3(mousePos.x, transform.position.y + riseSpeed * Time.fixedDeltaTime, 0f);
            rb.MovePosition(Vector3.Lerp(transform.position, target, 0.5f));

            if (transform.position.y >= startPos.y)
            {
                // Reset at top
                isRising = false;
                transform.position = startPos;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only catch fish while rising & if we have space
        if (isRising && caughtFishList.Count < maxCaughtFish)
        {
            Fish fish = other.GetComponent<Fish>();
            if (fish != null)
            {
                caughtFishList.Add(fish);

                // Parent fish to hookTip so it moves together with the hook
                fish.transform.SetParent(hookTip, worldPositionStays: false);

                // Position fish stacked under the hook tip
                float offsetX = 0.05f * caughtFishList.Count; // adjust spacing if needed
                fish.transform.localPosition = new Vector3(offsetX, 0f, 0f);

                // Rotate fish to face downward
                fish.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

                // Disable its physics so it doesn’t fall off
                Rigidbody2D frb = fish.GetComponent<Rigidbody2D>();
                if (frb != null) frb.simulated = false;

                float hookFishScale = 0.3f;
                fish.transform.localScale = Vector3.one * hookFishScale;
            }
        }
    }

    private void SellFish()
    {
        int total = 0;
        if (caughtFishList != null)
        {
            foreach (var f in caughtFishList)
            {
                if (f != null)
                {
                    Debug.Log($"Selling fish {f.name}, value = {f.value}");
                    total += f.value;
                    Destroy(f.gameObject);
                }
            }

        }
        else
        {
            Debug.LogWarning("caughtFishList was null when trying to sell fish");
        }
    

        if (moneyManager != null)
            moneyManager.AddMoney(total);

         caughtFishList = new List<Fish>();

        if (hookTip != null)
        {
            foreach(Transform child in hookTip)
            {
                Destroy(child.gameObject);
            }
        }
        

        if (fishSpawner != null)
            {
                fishSpawner.SpawnAllLayers();
            }
    }

    

    public void SetMaxCaughtFish(int newMax)
    {
        maxCaughtFish = newMax;
    }
}
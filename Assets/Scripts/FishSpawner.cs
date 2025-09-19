using Unity.VisualScripting;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class Fishlayer
    {
        public GameObject[] fishPrefabs;
        public float minY;
        public float maxY;
        public int maxFish = 5;

        [HideInInspector] public int currentFish;
    }
    public Fishlayer[] layers;
    public float spawnInterval = 0.1f;
    public float minX = -8f, maxX = 8f;

    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Spawnfish();
            timer = 0f;
        }
    }

    void Spawnfish()
    {
        foreach (Fishlayer layer in layers)
        {
            if (layer.currentFish >= layer.maxFish) continue;
            GameObject prefab = layer.fishPrefabs[Random.Range(0, layer.fishPrefabs.Length)];

            float X = Random.Range(minX, maxX);
            float Y = Random.Range(layer.minY, layer.maxY);
            Vector3 spawnPos = new Vector3(X, Y, 0);

            GameObject fish = Instantiate(prefab, spawnPos, Quaternion.identity);

            layer.currentFish++;

            FishTracker tracker = fish.AddComponent<FishTracker>();
            tracker.layer = layer;
        }

    }
    public void SpawnAllLayers()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Spawnfish();
    }

    // Update is called once per frame

}
public class FishTracker : MonoBehaviour
{
    [HideInInspector] public FishSpawner.Fishlayer layer;

    void OnDestroy()
    {
        if (layer != null)
            layer.currentFish--;
    }
}

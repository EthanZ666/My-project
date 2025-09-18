using UnityEngine;

public class HookCatcher : MonoBehaviour
{
    public MoneyManager moneyManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other)
    {
        Fish fish = other.GetComponent<Fish>();
        if (fish != null)
        {
            moneyManager.AddMoney(fish.value);
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

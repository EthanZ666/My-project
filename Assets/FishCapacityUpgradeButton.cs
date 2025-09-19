
using UnityEngine;
using UnityEngine.UI;

public class CapacityUpgrade : MonoBehaviour
{
    int money = 100;
    int level = 1;
    int fishAmount = 1;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(UpgradeCapacity);
    }
    
    void UpgradeCapacity()
    {
        if (money >= 30) {
            money -= 30;
            level++;
            fishAmount++;
            Debug.Log("Capacity upgraded to level " + level + " - can hold " + fishAmount + " fish");
        }
    }
}
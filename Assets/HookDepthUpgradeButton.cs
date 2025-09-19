using UnityEngine;
using UnityEngine.UI;

public class DepthUpgrade : MonoBehaviour
{
    int money = 100;
    int level = 1;
    int depth = 10;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(UpgradeDepth);
    }
    
    void UpgradeDepth()
    {
        if (money >= 50) {
            money -= 50;
            level++;
            depth += 10;
            Debug.Log("Depth upgraded to level " + level + " - can reach " + depth + "m deep");
        }
    }
}
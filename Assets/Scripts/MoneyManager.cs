using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text moneyText;
    private int money;
    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = "$" + money;
    }
}

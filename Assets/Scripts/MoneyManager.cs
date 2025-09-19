using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public TMP_Text moneyText;
    [SerializeField]private int money;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(this);
            return;
        }
        if (moneyText == null)
        {
            moneyText = GetComponentInChildren<TMP_Text>();
            if (moneyText != null) Debug.Log("MoneyManager: auto-found moneyText: " + moneyText.name);
        }
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("MoneyManager.AddMoney called, total=" + money);
        UpdateUI();

    }

    public void UpdateUI()
    {
        if (moneyText == null)
        {
            Debug.LogError("MoneyManager.UpdateUI: MoneyText is Null!");
            return;
        }
        moneyText.text = "$" + money;
    }
    void Start()
    {
        moneyText.text = "$" + money;
    }
}
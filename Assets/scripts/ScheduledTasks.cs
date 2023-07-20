using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ScheduledTasks : MonoBehaviour
{
    // DATA HOLDER DEFINITION
    // GUI | UI DEFINITIONS
    public Text moneyTextBox;
    // Events
    public delegate void MoneyUpdatedDelegate(float money);
    public static event MoneyUpdatedDelegate OnMoneyUpdated;
    // Methods definitions
    void passiveEarnings()
    {
        DataHolder.userMoney += DataHolder.userPassiveEarning;
        // Notify listeners that money has been updated
        OnMoneyUpdated?.Invoke(DataHolder.userMoney);
    }
    void updateMoneyText()
    {
        moneyTextBox.text = DataHolder.userMoney.ToString();
    }
    // Schedule definitions
    void Start()
    {
        InvokeRepeating("passiveEarnings", 1f, 1f); //Calling after 1 second and every 1 second after
        InvokeRepeating("updateMoneyText", 0f, 0.1f); //Clueless, need to be tended a little be later
    }
}
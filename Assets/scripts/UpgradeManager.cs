using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public Text clickPowerCostText;
    public Text passiveEarningCostText;
    public Button clickPowerButton;
    public Button passiveEarningButton;

    public DataHolder dataHolder; // —сылка на экземпл€р DataHolder

    [SerializeField] private List<float> clickPowerUpgradesCost;
    [SerializeField] private List<float> clickPowerUpgradesValues;
    [SerializeField] private List<float> passiveEarningUpgradesCost;
    [SerializeField] private List<float> passiveEarningUpgradesValues;

    private void Start()
    {
        // ѕрисваивание значений списков через ссылку на экземпл€р DataHolder
        DataHolder.clickPowerUpgradesCost = clickPowerUpgradesCost;
        DataHolder.clickPowerUpgradesValues = clickPowerUpgradesValues;
        DataHolder.passiveEarningUpgradesCost = passiveEarningUpgradesCost;
        DataHolder.passiveEarningUpgradesValues = passiveEarningUpgradesValues;
    }

    private void OnEnable()
    {
        ScheduledTasks.OnMoneyUpdated += UpdateUpgradeButtons;
    }

    private void OnDisable()
    {
        ScheduledTasks.OnMoneyUpdated -= UpdateUpgradeButtons;
    }

    public void UpdateUpgradeButtons(float money)
    {
        if (DataHolder.clickPowerIndex < DataHolder.clickPowerUpgradesCost.Count)
        {
            float clickPowerUpgradeCost = DataHolder.clickPowerUpgradesCost[DataHolder.clickPowerIndex];
            clickPowerCostText.text = "Cost: $" + clickPowerUpgradeCost.ToString();

            if (money >= clickPowerUpgradeCost)
            {
                clickPowerButton.interactable = true;
            }
            else
            {
                clickPowerButton.interactable = false;
            }
        }
        else
        {
            clickPowerCostText.text = "Max";
            clickPowerButton.interactable = false;
        }

        if (DataHolder.passiveEarningIndex < DataHolder.passiveEarningUpgradesCost.Count)
        {
            float passiveEarningUpgradeCost = DataHolder.passiveEarningUpgradesCost[DataHolder.passiveEarningIndex];
            passiveEarningCostText.text = "Cost: $" + passiveEarningUpgradeCost.ToString();

            if (money >= passiveEarningUpgradeCost)
            {
                passiveEarningButton.interactable = true;
            }
            else
            {
                passiveEarningButton.interactable = false;
            }
        }
        else
        {
            passiveEarningCostText.text = "Max";
            passiveEarningButton.interactable = false;
        }
    }

    public void PurchaseClickPowerUpgrade()
    {
        if (DataHolder.clickPowerIndex < DataHolder.clickPowerUpgradesCost.Count && DataHolder.userMoney >= DataHolder.clickPowerUpgradesCost[DataHolder.clickPowerIndex])
        {
            DataHolder.userMoney -= DataHolder.clickPowerUpgradesCost[DataHolder.clickPowerIndex];
            DataHolder.userClickPower += DataHolder.clickPowerUpgradesValues[DataHolder.clickPowerIndex];
            DataHolder.clickPowerIndex++;
            UpdateUpgradeButtons(DataHolder.userMoney);
        }
    }

    public void PurchasePassiveEarningUpgrade()
    {
        if (DataHolder.passiveEarningIndex < DataHolder.passiveEarningUpgradesCost.Count && DataHolder.userMoney >= DataHolder.passiveEarningUpgradesCost[DataHolder.passiveEarningIndex])
        {
            DataHolder.userMoney -= DataHolder.passiveEarningUpgradesCost[DataHolder.passiveEarningIndex];
            DataHolder.userPassiveEarning += DataHolder.passiveEarningUpgradesValues[DataHolder.passiveEarningIndex];
            DataHolder.passiveEarningIndex++;
            UpdateUpgradeButtons(DataHolder.userMoney);
        }
    }
}

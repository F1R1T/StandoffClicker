using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DataHolder
{
    public static float userPassiveEarning = 1f;
    public static float userClickPower = 1f;
    public static float userMoney = 0f;
    public static int donateMoney; // TODO: Better to keep this on the server-side

    public static List<float> clickPowerUpgradesCost = new List<float>();
    public static List<float> clickPowerUpgradesValues = new List<float>();
    public static List<float> passiveEarningUpgradesCost = new List<float>();
    public static List<float> passiveEarningUpgradesValues = new List<float>();

    public static int clickPowerIndex = 0;
    public static int passiveEarningIndex = 0;
}
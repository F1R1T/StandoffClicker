using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject("DataManager");
                    instance = go.AddComponent<DataManager>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "info.data");
        HookSaveData();
        InvokeRepeating("saveData", 0f, 1f);
    }

    public void saveData()
    {
        float[] dataToSave = {
            DataHolder.userMoney,
            DataHolder.userClickPower,
            DataHolder.userPassiveEarning,
            DataHolder.donateMoney,
            DataHolder.clickPowerIndex,
            DataHolder.passiveEarningIndex
        };

        List<float> clickPowerUpgradesCost = DataHolder.clickPowerUpgradesCost;
        List<float> clickPowerUpgradesValues = DataHolder.clickPowerUpgradesValues;
        List<float> passiveEarningUpgradesCost = DataHolder.passiveEarningUpgradesCost;
        List<float> passiveEarningUpgradesValues = DataHolder.passiveEarningUpgradesValues;

        // Convert lists to arrays for serialization
        float[] clickPowerCostArray = clickPowerUpgradesCost.ToArray();
        float[] clickPowerValueArray = clickPowerUpgradesValues.ToArray();
        float[] passiveEarningCostArray = passiveEarningUpgradesCost.ToArray();
        float[] passiveEarningValueArray = passiveEarningUpgradesValues.ToArray();

        // Append the arrays to the dataToSave array
        dataToSave = dataToSave.Concat(clickPowerCostArray).ToArray();
        dataToSave = dataToSave.Concat(clickPowerValueArray).ToArray();
        dataToSave = dataToSave.Concat(passiveEarningCostArray).ToArray();
        dataToSave = dataToSave.Concat(passiveEarningValueArray).ToArray();

        FileStream dataStream = new FileStream(savePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, dataToSave);
        dataStream.Close();
        Debug.Log("#DebugHelper: Called saveData - should be saved in " + savePath);
    }

    public void HookSaveData()
    {
        if (File.Exists(savePath))
        {
            FileStream dataStream = new FileStream(savePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            float[] savedData = converter.Deserialize(dataStream) as float[];
            dataStream.Close();

            DataHolder.userMoney = savedData[0];
            DataHolder.userClickPower = savedData[1];
            DataHolder.userPassiveEarning = savedData[2];
            DataHolder.donateMoney = (int)savedData[3];
            DataHolder.clickPowerIndex = (int)savedData[4];
            DataHolder.passiveEarningIndex = (int)savedData[5];

            // Retrieve the arrays from the saved data
            float[] clickPowerCostArray = savedData.Skip(6).Take(DataHolder.clickPowerUpgradesCost.Count).ToArray();
            float[] clickPowerValueArray = savedData.Skip(6 + DataHolder.clickPowerUpgradesCost.Count).Take(DataHolder.clickPowerUpgradesValues.Count).ToArray();
            float[] passiveEarningCostArray = savedData.Skip(6 + DataHolder.clickPowerUpgradesCost.Count + DataHolder.clickPowerUpgradesValues.Count).Take(DataHolder.passiveEarningUpgradesCost.Count).ToArray();
            float[] passiveEarningValueArray = savedData.Skip(6 + DataHolder.clickPowerUpgradesCost.Count + DataHolder.clickPowerUpgradesValues.Count + DataHolder.passiveEarningUpgradesCost.Count).ToArray();

            // Convert arrays back to lists
            DataHolder.clickPowerUpgradesCost = new List<float>(clickPowerCostArray);
            DataHolder.clickPowerUpgradesValues = new List<float>(clickPowerValueArray);
            DataHolder.passiveEarningUpgradesCost = new List<float>(passiveEarningCostArray);
            DataHolder.passiveEarningUpgradesValues = new List<float>(passiveEarningValueArray);
        }
        else
        {
            Debug.Log("Save file not found: " + savePath);
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Calling on exit: ");
        saveData();
    }
}

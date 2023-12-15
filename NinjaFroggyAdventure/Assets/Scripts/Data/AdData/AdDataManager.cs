using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AdDataManager : MonoBehaviour
{
    private string adSaveFile;
    private AdData adData;
    private AdInfo adInfo;
    private int length;

    private void Awake()
    {
        adSaveFile = Application.persistentDataPath + "/AdData.json";
        adInfo = GameObject.Find("DataManager").GetComponent<AdInfo>();
        LoadData();
    }

    public void LoadData()
    {
        if (File.Exists(adSaveFile))
        {
            string content = File.ReadAllText(adSaveFile);
            adData = JsonUtility.FromJson<AdData>(content);

            DateTime currentDate = DateTime.Now;
            length = adData.idList.Length;
            adInfo.idList = adData.idList;
            adInfo.nameList = adData.nameList;
            adInfo.endDateList = new DateTime[length];
            adInfo.rewardTypeList = adData.rewardTypeList;
            adInfo.amountList = adData.amountList;

            for (int i = 0; i < length; i++)
            {
                string cd = adData.endDateList[i];

                if (cd != "")
                {
                    adInfo.endDateList[i] = DateTime.Parse(cd);
                }
                else
                {
                    adInfo.endDateList[i] = currentDate;
                }
            }
        }
    }

    public void SaveData()
    {
        string[] idList = adInfo.idList;
        string[] nameList = adInfo.nameList;
        string[] endDateList = new string[length];
        RewardType.Type[] elementTypeList = adInfo.rewardTypeList;
        int[] amountList = adInfo.amountList;

        for (int i = 0; i < length; i++)
        {
            endDateList[i] = adInfo.endDateList[i].ToString();
        }

        AdData newAdData = new AdData()
        {
            idList = idList,
            nameList = nameList,
            endDateList = endDateList,
            rewardTypeList = elementTypeList,
            amountList = amountList,
        };

        string jsonChain = JsonUtility.ToJson(newAdData);
        File.WriteAllText(adSaveFile, jsonChain);
        Debug.Log("ad data save successfully");
    }
}

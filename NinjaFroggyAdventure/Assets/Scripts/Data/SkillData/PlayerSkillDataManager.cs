using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerSkillDataManager : MonoBehaviour
{
    public int briefs;
    public int totalSkills;
    public int totalSelectedSkills = 2;
    [SerializeField] private List<SkillInfoTemplate> skillsInfo;
    private PlayerSkillData playerSkillData;
    private GameData gameData;
    private PlayerSkillInfo playerSkillInfo;
    private string skillSaveFile;
    private string playerSaveFile;

    private void Awake()
    {
        skillSaveFile = Application.persistentDataPath + "/SkillData.json";
        playerSaveFile = Application.persistentDataPath + "/PlayerGameData.json";
        playerSkillInfo = GameObject.Find("DataManager").GetComponent<PlayerSkillInfo>();
        playerSkillData = new PlayerSkillData();
        LoadData();
    }

    public void BuySkill(string skillName)
    {
        for (int i = 0; i < playerSkillInfo.skillNameList.Length; i++)
        {
            if (playerSkillInfo.skillNameList[i] == skillName)
            {
                playerSkillInfo.isEnabledList[i] = true;
            }
        }
    }

    public bool GetSkillAvailability(string skillName)
    {
        for (int i = 0; i < playerSkillInfo.skillNameList.Length; i++)
        {
            if (playerSkillInfo.skillNameList[i] == skillName)
            {
                return playerSkillInfo.isEnabledList[i];
            }
        }

        return false;
    }

    public void LoadData()
    {
        if (File.Exists(skillSaveFile))
        {
            string content = File.ReadAllText(skillSaveFile);
            playerSkillData = JsonUtility.FromJson<PlayerSkillData>(content);

            playerSkillInfo.skillNameList = playerSkillData.skillNameList;
            playerSkillInfo.isEnabledList = playerSkillData.isEnabledList;
            playerSkillInfo.selectedSkillList = playerSkillData.selectedSkillList;

            if (File.Exists(playerSaveFile))
            {
                content = File.ReadAllText(playerSaveFile);
                gameData = JsonUtility.FromJson<GameData>(content);
                briefs = gameData.briefs;
            }
        }
        else
        {
            //if (GameObject.Find("SkillsStore"))
            //{
            //    SkillsStore skillsStore = GameObject.Find("SkillsStore").GetComponent<SkillsStore>();

            //    if (skillsStore)
            //    {
            //        totalSkills = skillsStore.skillsInfo.Count;
            //        playerSkillInfo.skillNameList = new string[totalSkills];
            //        playerSkillInfo.isEnabledList = new bool[totalSkills];
            //        playerSkillInfo.selectedSkillList = new string[totalSelectedSkills];

            //        for (int i = 0; i < totalSkills; i++)
            //        {
            //            var item = skillsStore.skillsInfo[i];
            //            playerSkillInfo.skillNameList[i] = item.title;
            //            playerSkillInfo.isEnabledList[i] = false;
            //        }

            //        for (int i = 0; i < totalSelectedSkills; i++)
            //        {
            //            playerSkillInfo.selectedSkillList[i] = "None";
            //        }

            //        if (File.Exists(playerSaveFile))
            //        {
            //            string content = File.ReadAllText(playerSaveFile);
            //            gameData = JsonUtility.FromJson<GameData>(content);
            //            briefs = gameData.briefs;
            //        }
            //    }
            //}
            //else
            //{
                playerSkillInfo.skillNameList = new string[totalSkills];
                playerSkillInfo.isEnabledList = new bool[totalSkills];
                playerSkillInfo.selectedSkillList = new string[totalSelectedSkills];

                for (int i = 0; i < totalSkills; i++)
                {
                    var item = skillsInfo[i];
                    playerSkillInfo.skillNameList[i] = item.title;
                    playerSkillInfo.isEnabledList[i] = false;
                }

                //playerSkillInfo.skillNameList[0] = "Dash";
                //playerSkillInfo.skillNameList[1] = "Triple Jump";
                //playerSkillInfo.skillNameList[2] = "Firy";
                //playerSkillInfo.skillNameList[3] = "Frost Guardian";

                for (int i = 0; i < totalSelectedSkills; i++)
                {
                    playerSkillInfo.selectedSkillList[i] = "None";
                }

                if (File.Exists(playerSaveFile))
                {
                    string content = File.ReadAllText(playerSaveFile);
                    gameData = JsonUtility.FromJson<GameData>(content);
                    briefs = gameData.briefs;
                }
            //}
        }
    }

    public void SaveData()
    {
        GameData newGameData = new GameData()
        {
            levelCompleted = gameData.levelCompleted,
            checkpointReachedLevel = gameData.checkpointReachedLevel,
            startLevel = gameData.startLevel,

            fruits = gameData.fruits,
            gems = gameData.gems,
            briefs = briefs,
            lifes = gameData.lifes,

            currentHearts = gameData.currentHearts,
            currentActiveHearts = gameData.currentActiveHearts,
            maxHearts = gameData.maxHearts,
        };

        PlayerSkillData newSkillData = new PlayerSkillData()
        {
            skillNameList = playerSkillInfo.skillNameList,
            isEnabledList = playerSkillInfo.isEnabledList,
            selectedSkillList = playerSkillInfo.selectedSkillList,
        };

        string jsonChain = JsonUtility.ToJson(newGameData);
        File.WriteAllText(playerSaveFile, jsonChain);
        jsonChain = JsonUtility.ToJson(newSkillData);
        File.WriteAllText(skillSaveFile, jsonChain);
    }
}

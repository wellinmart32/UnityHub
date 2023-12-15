using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class MainMenu : MonoBehaviour
{
    public AudioSource[] clips;
    public MainMenuMessage mainMenuMessage;
    public float waitTime;
    [SerializeField] private string storeRoute;
    [SerializeField] private string creditsRoute;
    [SerializeField, TextArea(4, 6)] private string[] messages;
    private string playerSaveFile;
    private string adSaveFile;

    private void Awake()
    {
        playerSaveFile = Application.persistentDataPath + "/PlayerGameData.json";
        adSaveFile = Application.persistentDataPath + "/AdData.json";
    }

    public void ConfirmNewGame()
    {
        if (!File.Exists(playerSaveFile))
        {
            clips[0].Play();
            NewGame();
        }
        else
        {
            clips[1].Play();
            mainMenuMessage.isImportant = true;
            mainMenuMessage.selectedOption = 1;
            mainMenuMessage.Show(messages[0], false);
        }
    }

    public void NewGame()
    {
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);

        if (di != null)
        {
            FileInfo[] files = di.GetFiles();

            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }

        GenerateNewFile();
        clips[0].Play();
        SceneManager.LoadScene("Level01");
    }

    public void SetAdData()
    {
        int length = 6;
        string[] idList = new string[length];
        string[] nameList = new string[length];
        string[] endDateList = new string[length];
        RewardType.Type[] rewardTypeList = new RewardType.Type[length];
        int[] amountList = new int[length];
        //idList[0] = "ca-app-pub-3940256099942544/5224354917";
        //idList[1] = "ca-app-pub-3940256099942544/1033173712";
        idList[0] = "ca-app-pub-6704680466944433/8207866966";
        idList[1] = "ca-app-pub-6704680466944433/8045119390";
        idList[2] = "ca-app-pub-6704680466944433/3605265219";
        idList[3] = "ca-app-pub-6704680466944433/6889904106";
        idList[4] = "ca-app-pub-6704680466944433/3343457052";
        idList[5] = "ca-app-pub-6704680466944433/4792771107";
        nameList[0] = "5GemsPackReward";
        nameList[1] = "5BriefsPackReward";
        nameList[2] = "BasicSwordReward";
        nameList[3] = "LightBallReward";
        nameList[4] = "SmallPotionReward";
        nameList[5] = "RandomCoinReward";
        rewardTypeList[0] = RewardType.Type.GemsPack;
        rewardTypeList[1] = RewardType.Type.BriefsPack;
        rewardTypeList[2] = RewardType.Type.BasicSword;
        rewardTypeList[3] = RewardType.Type.LightBall;
        rewardTypeList[4] = RewardType.Type.SmallPotion;
        rewardTypeList[5] = RewardType.Type.RandomCoinReward;
        amountList[0] = 5;
        amountList[1] = 5;
        amountList[2] = 1;
        amountList[3] = 1;
        amountList[4] = 1;
        amountList[5] = 5;

        AdData newAddData = new AdData()
        {
            idList = idList,
            nameList = nameList,
            endDateList = endDateList,
            rewardTypeList = rewardTypeList,
            amountList = amountList,
        };

        string jsonChain = JsonUtility.ToJson(newAddData);
        File.WriteAllText(adSaveFile, jsonChain);
    }

    private void GenerateNewFile()
    {
        GameData newPlayerData = new GameData()
        {
            checkpointReachedLevel = "Level01",
            levelCompleted = false,
            startLevel = true,

            fruits = 0,
            gems = 0,
            briefs = 0,
            lifes = 4,

            currentHearts = 3,
            currentActiveHearts = 3,
            maxHearts = 10,
        };

        string jsonChain = JsonUtility.ToJson(newPlayerData);
        File.WriteAllText(playerSaveFile, jsonChain);
        SetAdData();
    }

    public void LoadGame()
    {
        if (File.Exists(playerSaveFile))
        {
            string content = File.ReadAllText(playerSaveFile);
            GameData gameData = JsonUtility.FromJson<GameData>(content);

            if (gameData.lifes >= 1)
            {
                clips[0].Play();
                SceneManager.LoadScene(gameData.checkpointReachedLevel);
            }
            else
            {
                clips[2].Play();
                mainMenuMessage.selectedOption = 0;
                mainMenuMessage.Show(messages[1], true);
            }
        }
        else
        {
            clips[2].Play();
            mainMenuMessage.selectedOption = 0;
            mainMenuMessage.Show(messages[2], true);
        }
    }

    public void Load(GameData gameData)
    {
        SceneManager.LoadScene(gameData.checkpointReachedLevel);
    }

    public void OpenStore()
    {
        if (File.Exists(playerSaveFile))
        {
            clips[1].Play();
            StartCoroutine(OpenRoute(storeRoute));
        }
        else
        {
            clips[2].Play();
            mainMenuMessage.selectedOption = 0;
            mainMenuMessage.Show(messages[2], true);
        }
    }

    public void OpenCredits()
    {
        clips[1].Play();
        StartCoroutine(OpenRoute(creditsRoute));
    }

    private IEnumerator OpenRoute(string route)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(route);
    }

    public void QuitGame()
    {
        clips[1].Play();
        Application.Quit();
    }
}

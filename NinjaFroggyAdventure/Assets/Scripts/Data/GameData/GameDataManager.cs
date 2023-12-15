using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public string playerSaveFile;
    public string levelSaveFile;
    public GameData gameData;
    public string currentLevel;
    public string checkpointReachedLevel;
    public bool levelCompleted = false;
    private bool canLoadData = true;
    private bool startLevel;
    private GameObject player;
    private PlayerInfo playerInfo;
    private TransitionImage transitionImage;
    private CheckpointManager checkpointManager;
    private EnvironmentInfo environmentInfo;
    private TimeController timeController;

    private void Awake()
    {
        gameData = new GameData();
        currentLevel = SceneManager.GetActiveScene().name;
        playerSaveFile = Application.persistentDataPath + "/PlayerGameData.json";
        levelSaveFile = Application.persistentDataPath + "/" + currentLevel + "GameData"  + ".json";
        timeController = GameObject.Find("StandarInterface").GetComponent<Initialization>().timeController;
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        environmentInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().environmentInfo;
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        checkpointManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().checkpointManager;
        transitionImage = GameObject.Find("StandarInterface").GetComponent<Initialization>().transitionImage;
    }

    private void Start()
    {
        transitionImage.StartTransition(0f, 1, 3);
    }

    private void Update()
    {
        StartLevelLoadData();

        if (canLoadData)
        {
            timeController.StopTime();
            LoadData();
            player.transform.position = checkpointManager.checkPoints[checkpointManager.lastCheckpointReached].GetComponent<Checkpoint>().respawnPosition;
            player.GetComponent<SpriteRenderer>().flipX = checkpointManager.checkPoints[checkpointManager.lastCheckpointReached].GetComponent<Checkpoint>().respawnPlayerFlip;
            canLoadData = false;
            timeController.RestoreTime();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //LoadData();
            //player.transform.position = checkpointManager.checkPoints[checkpointManager.lastCheckpointReached].GetComponent<Checkpoint>().respawnPosition;
            //player.GetComponent<SpriteRenderer>().flipX = checkpointManager.checkPoints[checkpointManager.lastCheckpointReached].GetComponent<Checkpoint>().respawnPlayerFlip;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //SaveData();
        }
    }

    private void StartLevelLoadData()
    {
        if (File.Exists(playerSaveFile))
        {
            string content = File.ReadAllText(playerSaveFile);
            gameData = JsonUtility.FromJson<GameData>(content);
            startLevel = gameData.startLevel;
            
            if (!File.Exists(levelSaveFile))
            {
                playerInfo.fruits = gameData.fruits;
                playerInfo.gems = gameData.gems;
                playerInfo.briefs = gameData.briefs;
                playerInfo.lifes = gameData.lifes;

                playerInfo.currentHearts = gameData.currentHearts;
                playerInfo.currentActiveHearts = gameData.currentActiveHearts;
                playerInfo.maxHearts = gameData.maxHearts;
                playerInfo.SetActiveHearts();

                currentLevel = SceneManager.GetActiveScene().name;
                checkpointReachedLevel = currentLevel;
                levelCompleted = false;
                SaveData();
                player.transform.position = checkpointManager.checkPoints[checkpointManager.lastCheckpointReached].GetComponent<Checkpoint>().respawnPosition;
                player.GetComponent<SpriteRenderer>().flipX = checkpointManager.checkPoints[checkpointManager.lastCheckpointReached].GetComponent<Checkpoint>().respawnPlayerFlip;
            }
        }
    }

    public void LoadData()
    {
        if (File.Exists(playerSaveFile))
        {
            string content = File.ReadAllText(playerSaveFile);
            gameData = JsonUtility.FromJson<GameData>(content);
            playerInfo.fruits = gameData.fruits;
            playerInfo.gems = gameData.gems;
            playerInfo.briefs = gameData.briefs;
            playerInfo.lifes = gameData.lifes;

            playerInfo.currentHearts = gameData.currentHearts;
            playerInfo.currentActiveHearts = gameData.currentActiveHearts;
            playerInfo.maxHearts = gameData.maxHearts;

            playerInfo.SetActiveHearts();

            checkpointReachedLevel = gameData.checkpointReachedLevel;
        }

        if (File.Exists(levelSaveFile))
        {
            string content = File.ReadAllText(levelSaveFile);
            gameData = JsonUtility.FromJson<GameData>(content);
            environmentInfo.RestoreEnvironmentObjects(gameData);
            checkpointManager.lastCheckpointReached = gameData.lastCheckpointReached;
        }
    }

    public void SaveData()
    {
        environmentInfo.SetEnvironmentData();

        int fruitsLength = environmentInfo.fruitsDataList.Length;
        int gemsLength = environmentInfo.gemsDataList.Length;
        int briefsLength = environmentInfo.briefsDataList.Length;
        int lifesLength = environmentInfo.lifesDataList.Length;
        int boxesLength = environmentInfo.boxesDataList.Length;
        int itemsLength = environmentInfo.itemsDataList.Length;
        int heartsLength = environmentInfo.heartsDataList.Length;
        int reachedCheckpointsLength = environmentInfo.reachedCheckpointsDataList.Length;

        bool[] fpu = new bool[fruitsLength];
        bool[] gpu = new bool[gemsLength];
        bool[] bpu = new bool[briefsLength];
        bool[] lpu = new bool[lifesLength];
        bool[] hpu = new bool[heartsLength];
        bool[] bd = new bool[boxesLength];
        bool[] ipu = new bool[itemsLength];
        bool[] rcp = new bool[reachedCheckpointsLength];

        for (int i = 0; i < fruitsLength; i++)
        {
            bool isCollected = environmentInfo.fruitsDataList[i];
            fpu[i] = isCollected;
        }

        for (int i = 0; i < gemsLength; i++)
        {
            bool isCollected = environmentInfo.gemsDataList[i];
            gpu[i] = isCollected;
        }

        for (int i = 0; i < briefsLength; i++)
        {
            bool isCollected = environmentInfo.briefsDataList[i];
            bpu[i] = isCollected;
        }

        for (int i = 0; i < lifesLength; i++)
        {
            bool isCollected = environmentInfo.lifesDataList[i];
            lpu[i] = isCollected;
        }

        for (int i = 0; i < heartsLength; i++)
        {
            bool isCollected = environmentInfo.heartsDataList[i];
            hpu[i] = isCollected;
        }

        for (int i = 0; i < boxesLength; i++)
        {
            bool isBroken = environmentInfo.boxesDataList[i];
            bd[i] = isBroken;
        }

        for (int i = 0; i < itemsLength; i++)
        {
            bool isCollected = environmentInfo.itemsDataList[i];
            ipu[i] = isCollected;
        }

        for (int i = 0; i < reachedCheckpointsLength; i++)
        {
            bool isReached = environmentInfo.reachedCheckpointsDataList[i];
            rcp[i] = isReached;
        }

        GameData newPlayerData = new GameData()
        {
            levelCompleted = levelCompleted,
            checkpointReachedLevel = checkpointReachedLevel,
            startLevel = startLevel,

            fruits = playerInfo.fruits,
            gems = playerInfo.gems,
            briefs = playerInfo.briefs,
            lifes = playerInfo.lifes,

            currentHearts = playerInfo.currentHearts,
            currentActiveHearts = playerInfo.currentActiveHearts,
            maxHearts = playerInfo.maxHearts,
        };

        GameData newLevelData = new GameData()
        {
            fruitsPickUp = fpu,
            briefsPickUp = bpu,
            gemsPickUp = gpu,
            lifesPickUp = lpu,
            heartsPickUp = hpu,
            boxesDestroyed = bd,
            itemsPickUp = ipu,
            reachedCheckpoints = rcp,

            lastCheckpointReached = checkpointManager.lastCheckpointReached,
        };

        string jsonChain = JsonUtility.ToJson(newPlayerData);
        File.WriteAllText(playerSaveFile, jsonChain);
        jsonChain = JsonUtility.ToJson(newLevelData);
        File.WriteAllText(levelSaveFile, jsonChain);
        Debug.Log("Save successfully");
    }

    public void SaveLifeData()
    {
        if (File.Exists(playerSaveFile))
        {
            string content = File.ReadAllText(playerSaveFile);
            gameData = JsonUtility.FromJson<GameData>(content);

            GameData newPlayerData = new GameData()
            {
                levelCompleted = gameData.levelCompleted,
                checkpointReachedLevel = gameData.checkpointReachedLevel,
                startLevel = gameData.startLevel,

                fruits = gameData.fruits,
                gems = gameData.gems,
                briefs = gameData.briefs,
                lifes = playerInfo.lifes,

                currentHearts = gameData.currentHearts,
                currentActiveHearts = gameData.currentActiveHearts,
                maxHearts = gameData.maxHearts,
            };

            string jsonChain = JsonUtility.ToJson(newPlayerData);
            File.WriteAllText(playerSaveFile, jsonChain);
        }
    }
}

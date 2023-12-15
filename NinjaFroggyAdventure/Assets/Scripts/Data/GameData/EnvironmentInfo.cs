using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInfo : MonoBehaviour
{
    private GameObject fruitManager;
    private GameObject briefManager;
    private GameObject gemManager;
    private GameObject lifeManager;
    private GameObject heartManager;
    private GameObject boxManager;
    private GameObject itemManager;
    private GameObject enemyManager;
    private GameObject chestManager;
    private GameObject recoveryManager;
    private CheckpointManager checkpointManager;

    public GameData gameData;

    public bool[] fruitsDataList;
    public bool[] gemsDataList;
    public bool[] briefsDataList;
    public bool[] lifesDataList;
    public bool[] heartsDataList;
    public bool[] boxesDataList;
    public bool[] itemsDataList;
    public bool[] reachedCheckpointsDataList;
    
    private void Awake()
    {
        fruitManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().fruitManager;
        briefManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().briefManager;
        gemManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().gemManager;
        lifeManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().LifeManager;
        heartManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().heartManager;
        boxManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().boxManager;
        itemManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().itemManager;
        enemyManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().enemyManager;
        chestManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().chestManager;
        checkpointManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().checkpointManager;

        SetEnvironmentData();
    }

    public void SetEnvironmentData()
    {
        SetFruitsData();
        SetGemsData();
        SetBriefsData();
        SetLifesData();
        SetHeartsData();
        SetBoxesData();
        SetItemsData();
        SetReachedCheckpointsData();
    }

    private void SetFruitsData()
    {
        int totalFruits = fruitManager.transform.childCount;
        fruitsDataList = new bool[totalFruits];

        for (int i = 0; i < totalFruits; i++)
        { 
            Transform fruit = fruitManager.transform.GetChild(i);
            fruitsDataList[i] = fruit.gameObject.GetComponent<FruitCollected>().isCollected;
        }
    }

    private void SetGemsData()
    {
        int totalGems = gemManager.transform.childCount;
        gemsDataList = new bool[totalGems];

        for (int i = 0; i < totalGems; i++)
        {
            Transform gem = gemManager.transform.GetChild(i);
            gemsDataList[i] = gem.gameObject.GetComponent<GemCollected>().isCollected;
        }
    }

    private void SetBriefsData()
    {
        int totalBriefs = briefManager.transform.childCount;
        briefsDataList = new bool[totalBriefs];

        for (int i = 0; i < totalBriefs; i++)
        {
            Transform brief = briefManager.transform.GetChild(i);
            briefsDataList[i] = brief.gameObject.GetComponent<BriefCollected>().isCollected;
        }
    }

    private void SetLifesData()
    {
        int totalLifes = lifeManager.transform.childCount;
        lifesDataList = new bool[totalLifes];

        for (int i = 0; i < totalLifes; i++)
        {
            Transform life = lifeManager.transform.GetChild(i);
            lifesDataList[i] = life.gameObject.GetComponent<LifeCollected>().isCollected;
        }
    }

    private void SetHeartsData()
    {
        int totalHearts = heartManager.transform.childCount;
        heartsDataList = new bool[totalHearts];

        for (int i = 0; i < totalHearts; i++)
        {
            Transform heart = heartManager.transform.GetChild(i);
            heartsDataList[i] = heart.gameObject.GetComponent<HeartCollected>().isCollected;
        }
    }

    private void SetBoxesData()
    {
        int totalBoxes = boxManager.transform.childCount;
        boxesDataList = new bool[totalBoxes];

        for (int i = 0; i < totalBoxes; i++)
        {
            Transform box = boxManager.transform.GetChild(i).transform.GetChild(0);
            boxesDataList[i] = box.gameObject.GetComponent<JumpBox>().isBroken;
        }
    }

    private void SetItemsData()
    {
        int totalItems = itemManager.transform.childCount;
        itemsDataList = new bool[totalItems];

        for (int i = 0; i < totalItems; i++)
        {
            Transform item = itemManager.transform.GetChild(i);
            itemsDataList[i] = item.gameObject.GetComponent<ItemCollected>().isCollected;
        }
    }

    private void SetReachedCheckpointsData()
    {
        int totalCheckpoints = checkpointManager.checkPoints.Length;
        reachedCheckpointsDataList = new bool[totalCheckpoints];

        for (int i = 0; i < totalCheckpoints; i++)
        {
            Checkpoint checkpoint = checkpointManager.checkPoints[i].GetComponent<Checkpoint>();
            reachedCheckpointsDataList[i] = checkpoint.isReached;
        }
    }

    public void RestoreEnvironmentObjects(GameData gamedata)
    {
        int totalFruits = fruitManager.transform.childCount;
        int totalGems = gemManager.transform.childCount;
        int totalBriefs = briefManager.transform.childCount;
        int totalLifes = lifeManager.transform.childCount;
        int totalHearts = heartManager.transform.childCount;
        int totalBoxes = boxManager.transform.childCount;
        int totalEnemies = enemyManager.transform.childCount;
        int totalCheckpoints = checkpointManager.checkPoints.Length;
        int totalItems = itemManager.transform.childCount;

        for (int i = 0; i < totalFruits; i++)
        {
            bool isCollected = gamedata.fruitsPickUp[i];
            Transform fruit = fruitManager.transform.GetChild(i);
            
            if (fruit.GetComponent<FruitCollected>().isCollected && !isCollected)
            {
                fruit.GetComponent<FruitCollected>().RestoreFruit();
            }

            if (isCollected)
            {
                fruit.GetComponent<FruitCollected>().HideFruit();
                fruit.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < totalGems; i++)
        {
            bool isCollected = gamedata.gemsPickUp[i];
            Transform gem = gemManager.transform.GetChild(i);

            if (gem.GetComponent<GemCollected>().isCollected && !isCollected)
            {
                gem.GetComponent<GemCollected>().RestoreGem();
            }

            if (isCollected)
            {
                gem.GetComponent<GemCollected>().HideGem();
                gem.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < totalBriefs; i++)
        {
            bool isCollected = gamedata.briefsPickUp[i];
            Transform brief = briefManager.transform.GetChild(i);

            if (brief.GetComponent<BriefCollected>().isCollected && !isCollected)
            {
                brief.GetComponent<BriefCollected>().RestoreBrief();
            }

            if (isCollected)
            {
                brief.GetComponent<BriefCollected>().HideBrief();
                brief.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < totalLifes; i++)
        {
            bool isCollected = gamedata.lifesPickUp[i];
            Transform life = lifeManager.transform.GetChild(i);

            if (life.GetComponent<LifeCollected>().isCollected && !isCollected)
            {
                life.GetComponent<LifeCollected>().RestoreLife();
            }

            if (isCollected)
            {
                life.GetComponent<LifeCollected>().HideLife();
                life.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < totalHearts; i++)
        {
            bool isCollected = gamedata.heartsPickUp[i];
            Transform heart = heartManager.transform.GetChild(i);

            if (heart.GetComponent<HeartCollected>().isCollected && !isCollected)
            {
                heart.GetComponent<HeartCollected>().RestoreHeart();
            }

            if (isCollected)
            {
                heart.GetComponent<HeartCollected>().HideHeart();
                heart.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < totalItems; i++)
        {
            bool isCollected = gamedata.itemsPickUp[i];
            Transform item = itemManager.transform.GetChild(i);

            if (item.GetComponent<ItemCollected>().isCollected && !isCollected)
            {
                item.GetComponent<ItemCollected>().RestoreItem();
            }

            if (isCollected)
            {
                item.GetComponent<ItemCollected>().HideItem();
                item.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < totalBoxes; i++)
        {
            bool isBroken = gamedata.boxesDestroyed[i];
            Transform box = boxManager.transform.GetChild(i).transform.GetChild(0);

            if (box.GetComponent<JumpBox>().isBroken && !isBroken)
            {
                box.GetComponent<JumpBox>().RestoreBox();
            }
        }

        for (int i = 0; i < totalEnemies; i++)
        {
            Transform enemy = enemyManager.transform.GetChild(i);
            enemy.GetComponent<RespawnEnemy>().RestoreEnemy();
        }

        for (int i = 0; i < totalCheckpoints; i++)
        {
            bool isReached = gamedata.reachedCheckpoints[i];
            Checkpoint checkpoint = checkpointManager.checkPoints[i].GetComponent<Checkpoint>();
            
            if (!checkpoint.isReached && isReached)
            {
                checkpoint.ActivateCheckpoint();
            }
        }
    }
}

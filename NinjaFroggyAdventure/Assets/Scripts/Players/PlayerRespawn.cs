using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject[] hearts;
    public float respawnTime = 0.5f;
    public float immuneTime = 0.5f;
    private GameDataManager gameDataManager;
    private InventoryDataManager inventoryDataManager;
    private CheckpointManager checkpointManager;
    private Animator animator;
    private PlayerInfo playerInfo;
    private TransitionImage transitionImage;
    private bool isImmune = false;
    private TimeController timeController;

    private void Awake()
    {
        timeController = GameObject.Find("StandarInterface").GetComponent<Initialization>().timeController;
        gameDataManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().gameDataManager;
        inventoryDataManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventoryDataManager;
        checkpointManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().checkpointManager;
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        transitionImage = GameObject.Find("StandarInterface").GetComponent<Initialization>().transitionImage;
        animator = GetComponent<Animator>();
    }

    private void CheckLifes()
    {
        if (playerInfo.currentHearts <= 0)
        {
            if (playerInfo.lifes <= 1)
            {
                transitionImage.StartTransition(3f, 10, 3f);
                gameDataManager.LoadData();
                inventoryDataManager.LoadData();
                playerInfo.lifes--;
                playerInfo.SetActiveHearts();
                gameDataManager.SaveData();
                SceneManager.LoadScene("GameOver");
                return;
            }
            else
            {
                timeController.StopTime();
                transitionImage.StartTransition(0.3f, 5, 3f);
                StartCoroutine(Respawn());
                return;
            }
        }

        for (int i = playerInfo.currentHearts; i < hearts.Length; i++)
        {
            hearts[i].GetComponent<Image>().color = Color.black;
        }
 
        animator.SetBool("Hit", true);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(respawnTime);
        gameDataManager.LoadData();
        inventoryDataManager.LoadData();
        playerInfo.lifes--;
        playerInfo.currentHearts = playerInfo.currentActiveHearts;
        playerInfo.SetActiveHearts();
        gameDataManager.SaveData();
        StartCoroutine(Translate());
    }

    private IEnumerator Translate()
    {
        yield return new WaitForSecondsRealtime(2);
        gameObject.transform.position = checkpointManager.checkPoints[checkpointManager.lastCheckpointReached].GetComponent<Checkpoint>().respawnPosition;
        gameObject.GetComponent<SpriteRenderer>().flipX = checkpointManager.checkPoints[checkpointManager.lastCheckpointReached].GetComponent<Checkpoint>().respawnPlayerFlip;
        timeController.RestoreTime();
    }

    public void PlayerDamaged(int damage, bool isSuperAttack)
    {
        if (!isSuperAttack)
        {
            if (!isImmune)
            {
                isImmune = true;
                playerInfo.currentHearts = playerInfo.currentHearts - damage;
                CheckLifes();
                StartCoroutine(DisableImmunity());
            }
        }
        else
        {
            playerInfo.currentHearts = playerInfo.currentHearts - damage;
            CheckLifes();
            StartCoroutine(DisableImmunity());
        }
    }

    private IEnumerator DisableImmunity()
    {
        yield return new WaitForSeconds(immuneTime);
        animator.SetBool("Hit", false);
        isImmune = false;
    }

    public void SetCurrentHearts(int currentHearts)
    {
        playerInfo.currentHearts = currentHearts;
    }

    public int GetCurrentHearts()
    {
        return playerInfo.currentHearts;
    }

    public int GetCurrentActiveHearts()
    {
        return playerInfo.currentActiveHearts;
    }

    public void SetCurrentActiveHearts(int currentActiveHearts)
    {
        playerInfo.currentActiveHearts = currentActiveHearts;
    }
}

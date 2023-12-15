using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public int checkpointPosition;
    public Vector2 respawnPosition;
    public bool respawnPlayerFlip;
    public bool isReached = false;
    private Animator animator;
    private CheckpointManager checkpointManager;
    private GameDataManager gameDataManager;
    private InventoryDataManager inventoryDataManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        checkpointManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().checkpointManager;
        gameDataManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().gameDataManager;
        inventoryDataManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventoryDataManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (!isReached)
            {
                ActivateCheckpoint();
                checkpointManager.lastCheckpointReached = checkpointPosition;
                gameDataManager.checkpointReachedLevel = SceneManager.GetActiveScene().name;
                gameDataManager.SaveData();
                inventoryDataManager.SaveData();
            }
        }
    }

    public void ActivateCheckpoint()
    {
        animator.SetBool("Checkpoint", true);
        isReached = true;
    }
}

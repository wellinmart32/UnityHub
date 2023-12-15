using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateCheckpoint : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    private GameDataManager gameDataManager;


    void Awake()
    {
        checkpointManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().checkpointManager;
        gameDataManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().gameDataManager;
        StartCoroutine(ReachCheckPoint());
    }


    private IEnumerator ReachCheckPoint()
    {
        yield return new WaitForSeconds(2);
        Checkpoint checkpoint = GetComponent<Checkpoint>();
        checkpoint.ActivateCheckpoint();
        
        checkpointManager.lastCheckpointReached = gameObject.GetComponent<Checkpoint>().checkpointPosition;
        gameDataManager.checkpointReachedLevel = SceneManager.GetActiveScene().name;
        gameDataManager.SaveData();
    }
}

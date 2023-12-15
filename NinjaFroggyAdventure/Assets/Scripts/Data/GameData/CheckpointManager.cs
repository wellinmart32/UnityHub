using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject[] checkPoints;
    public int lastCheckpointReached;

    private void Awake()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].GetComponent<Checkpoint>().checkpointPosition = i;
        }
    }
}

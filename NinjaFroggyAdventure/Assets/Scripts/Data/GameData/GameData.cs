using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string[,] levelsList;

    public bool levelCompleted;
    public string checkpointReachedLevel;
    public bool startLevel;

    // Player data
    public int fruits;
    public int gems;
    public int briefs;
    public int lifes;

    public int currentHearts;
    public int currentActiveHearts;
    public int maxHearts;

    // Environment data
    public bool[] fruitsPickUp;
    public bool[] gemsPickUp;
    public bool[] briefsPickUp;
    public bool[] lifesPickUp;
    public bool[] heartsPickUp;
    public bool[] boxesDestroyed;
    public bool[] itemsPickUp;
    public bool[] reachedCheckpoints;

    public int lastCheckpointReached;
}

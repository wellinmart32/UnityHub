using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitData
{
    public bool isCollected;
    public FruitCollected.Fruit type;
    public Vector2 position;

    public FruitData(bool isCollected, FruitCollected.Fruit type, Vector2 position)
    {
        this.isCollected = isCollected;
        this.type = type;
        this.position = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Template/AdInfo")]
public class AdInfoTemplate : ScriptableObject
{
    public int idPos;
    public string tittle;
    public Sprite image;
    public int amount;
    public int waitTime;
}

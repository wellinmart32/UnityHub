using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Template/SkillInfo")]
public class SkillInfoTemplate : ScriptableObject
{
    public string title;
    public string skillType;
    public string description;
    public int price;
}

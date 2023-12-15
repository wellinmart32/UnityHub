using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerSkill : MonoBehaviour
{
    public int pos;
    public GameObject templatesContainer;
    public PlayerSkillSelector playerSkillSelector;
    
    public void OpenPlayerSkillList()
    {
        playerSkillSelector.pos = pos;
        playerSkillSelector.ShowSkills();
    }
}

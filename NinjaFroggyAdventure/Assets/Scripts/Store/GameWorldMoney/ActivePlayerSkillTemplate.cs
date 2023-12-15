using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivePlayerSkillTemplate : MonoBehaviour
{
    public TextMeshProUGUI title;
    private PlayerSkillSelector playerSkillSelector;

    private void Awake()
    {
        playerSkillSelector = GameObject.Find("SkillSelector").GetComponent<PlayerSkillSelector>();
    }

    public void SelectPlayerSkill()
    {
        playerSkillSelector.SelectPlayerSkill(title.text);
    }
}

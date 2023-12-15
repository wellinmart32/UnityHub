using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public int skillPos;
    public GameObject playerSkills;
    private string skillName;
    private PlayerSkillInfo playerSkillInfo;
    private Dash dash;
    private Firy firy;
    private Feint feint;
    private FrostGuardian frostGuardian;

    private void Awake()
    {
        playerSkillInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerSkillInfo;
        dash = playerSkills.GetComponent<Dash>();
        firy = playerSkills.GetComponent<Firy>();
        feint = playerSkills.GetComponent<Feint>();
        frostGuardian = playerSkills.GetComponent<FrostGuardian>();
    }

    private void Start()
    {
        skillName = playerSkillInfo.selectedSkillList[skillPos];
    }

    public void ActivateSkill()
    {
        switch (skillName)
        {
            case "None":
                break;
            case "Dash":
                dash.EnableDash();
                break;
            case "Firy":
                firy.EnableFiry();
                break;
            case "Frost Guardian":
                frostGuardian.EnableTransform();
                break;
            case "Feint":
                feint.EnableFeint();
                break;
            default:
                break;
        }
    }
}

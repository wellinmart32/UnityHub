using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileAction : MonoBehaviour
{
    private Sword2D sword2D;
    private Ball ball;
    private GameObject weaponManager;
    private GameObject player;
    private GameObject frostGuardianAvatar;
    private PlayerMoveJoystick playerMoveJoystick;
    private FrostGuardianMove frostGuardianMove;
    private SummonIcePlatform summonIcePlatform;

    private void Awake()
    {
        weaponManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().weaponManager;
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        frostGuardianAvatar = GameObject.Find("StandarInterface").GetComponent<Initialization>().frostGuardianAvatar;
        frostGuardianMove = frostGuardianAvatar.GetComponent<FrostGuardianMove>();
        summonIcePlatform = frostGuardianAvatar.GetComponent<SummonIcePlatform>();
        playerMoveJoystick = player.GetComponent<PlayerMoveJoystick>();
        sword2D = weaponManager.transform.GetChild(0).GetComponentInChildren<Sword2D>();
        ball = weaponManager.transform.GetChild(1).GetComponentInChildren<Ball>();
    }

    public void WeaponAttack()
    {
        if (player.activeInHierarchy)
        {
            if (weaponManager.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                sword2D.Attack();
            }
            else if (weaponManager.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                ball.LaunchBall();
            }
        }
        else if (frostGuardianAvatar.activeInHierarchy)
        {
            frostGuardianMove.EnableAttack();
        }
    }

    public void MainAction()
    {
        if (player.activeInHierarchy)
        {
            playerMoveJoystick.Jump();
        }
        else if (frostGuardianAvatar.activeInHierarchy)
        {
            summonIcePlatform.EnableSummoning();
        }
    }
}

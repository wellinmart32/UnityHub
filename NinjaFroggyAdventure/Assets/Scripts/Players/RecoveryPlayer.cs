using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryPlayer : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
    }
    public void RecoveryByHeart()
    {
        GameObject[] hearts = gameObject.GetComponent<PlayerRespawn>().hearts;

        for (int i = 0; i < playerInfo.currentActiveHearts + 1; i++)
        {
            if (i < playerInfo.maxHearts)
            {
                hearts[i].GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void RecoveryByPotion(int recoveryHearts)
    {
        if (recoveryHearts + playerInfo.currentHearts < playerInfo.currentActiveHearts)
        {
            playerInfo.currentHearts = playerInfo.currentHearts + recoveryHearts;
        }
        else
        {
            playerInfo.currentHearts = playerInfo.currentActiveHearts;
        }

        playerInfo.SetActiveHearts();
    }
}

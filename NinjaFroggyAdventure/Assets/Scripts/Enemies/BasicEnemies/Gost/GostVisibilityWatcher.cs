using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostVisibilityWatcher : MonoBehaviour
{
    public GostVisibility gostVisibility;
    public bool canAppear;
    public float startTime = 5;
    private float waitTime;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
    }

    private void Update()
    {
        if (playerInfo.currentHearts < 1)
        {
            canAppear = false;
        }

        if (canAppear)
        {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                gostVisibility.AppearGost();
                waitTime = startTime;
                canAppear = false;
            }
        }
        else
        {
            waitTime = startTime;
        }
    }
}

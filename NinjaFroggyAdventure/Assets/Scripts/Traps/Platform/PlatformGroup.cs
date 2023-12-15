using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGroup : MonoBehaviour
{

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject platform = transform.GetChild(i).gameObject;
            platform.transform.GetChild(0).GetComponent<PlatformMove>().isSystem = true;
        }
    }

    private void Update()
    {
        bool isStopSystem = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject platform = transform.GetChild(i).gameObject;

            if (platform.transform.GetChild(0).GetComponent<PlatformMove>().canMove)
            {
                isStopSystem = false;
            }
        }

        if (isStopSystem)
        {
            StartMovement();
        }
    }


    private void StartMovement()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject platform = transform.GetChild(i).gameObject;
            platform.transform.GetChild(0).GetComponent<PlatformMove>().canMove = true;
        }
    }
}

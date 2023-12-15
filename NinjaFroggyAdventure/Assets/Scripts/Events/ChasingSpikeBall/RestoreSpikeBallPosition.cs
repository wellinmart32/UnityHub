using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreSpikeBallPosition : MonoBehaviour
{
    public GameObject spikeBall;
    public ChasingSpikeBall chasingSpikeBall;
    private PlayerInfo playerInfo;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = spikeBall.transform.position;
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
    }

    private void Update()
    {
        if (playerInfo.currentHearts <= 0)
        {
            RestoreInitialPosition();
        }
    }

    private void RestoreInitialPosition()
    {
        chasingSpikeBall.canMove = false;
        chasingSpikeBall.DisableSpikeBall();
        spikeBall.transform.position = initialPosition;
    }
}

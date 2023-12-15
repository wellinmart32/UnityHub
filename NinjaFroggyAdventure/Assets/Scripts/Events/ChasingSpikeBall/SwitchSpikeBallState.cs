using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSpikeBallState : MonoBehaviour
{
    public float waitTime;
    public ChasingSpikeBall chasingSpikeBall;
    public GameObject bridgeLock;
    public bool firstActivation = true;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && firstActivation)
        {
            firstActivation = false;
            bridgeLock.SetActive(false);
            chasingSpikeBall.canMove = true;

            if (player.GetComponent<PlayerMove>() != null)
            {
                player.GetComponent<PlayerMove>().canMove = false;
                StartCoroutine(player.GetComponent<PlayerMove>().EnableMove(waitTime));
            }

            if (player.GetComponent<PlayerMoveJoystick>() != null)
            {
                player.GetComponent<PlayerMoveJoystick>().canMove = false;
                StartCoroutine(player.GetComponent<PlayerMoveJoystick>().EnableMove(waitTime));
            }

        }
    }

    public void RestoreLock()
    {
        firstActivation = true;
        bridgeLock.SetActive(true);
    }
}

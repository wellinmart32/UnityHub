using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCamera : MonoBehaviour
{
    public bool xFreeze;
    public bool YFreeze;
    private Camera movingCamera;
    private Camera currentCamera;
    private Camera playerCamera;
    private Camera frostGuardianCamera;
    private GameObject player;
    private GameObject frostGuardianAvatar;
    private GameObject currentPlayer;
    private Vector2 stopPosition;
    private bool canReposition = true;
    private bool canStop = false;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        frostGuardianAvatar = GameObject.Find("StandarInterface").GetComponent<Initialization>().frostGuardianAvatar;
        GameObject cameraManager = GameObject.Find("CameraManager");
        movingCamera = cameraManager.transform.GetChild(cameraManager.transform.childCount - 1).gameObject.GetComponent<Camera>();
        playerCamera = player.GetComponentInChildren<Camera>();
        frostGuardianCamera = frostGuardianAvatar.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (canStop)
        {
            if (xFreeze && YFreeze)
            {
                movingCamera.transform.position = new Vector3(stopPosition.x, stopPosition.y, -1);
            }
            else
            {
                if (xFreeze)
                {
                    movingCamera.transform.position = new Vector3(stopPosition.x, currentPlayer.transform.position.y, -1);
                }

                if (YFreeze)
                {
                    movingCamera.transform.position = new Vector3(currentPlayer.transform.position.x, stopPosition.y, -1);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SettActivePlayer();
            SetActiveCamera();

            if (canReposition)
            {
                stopPosition = currentPlayer.transform.position;
                canReposition = false;
                canStop = true;
            }

            movingCamera.gameObject.SetActive(true);
            currentCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SettActivePlayer();
            SetActiveCamera();
            currentCamera.gameObject.SetActive(true);
            movingCamera.gameObject.SetActive(false);
            canReposition = true;
            canStop = false;
        }
    }

    private void SetActiveCamera()
    {
        if (frostGuardianAvatar.activeInHierarchy)
        {
            currentCamera = frostGuardianCamera;
        }
        else
        {
            currentCamera = playerCamera;
        }
    }

    private void SettActivePlayer()
    {
        if (frostGuardianAvatar.activeInHierarchy)
        {
            currentPlayer = frostGuardianAvatar;
        }
        else
        {
            currentPlayer = player;
        }
    }
}

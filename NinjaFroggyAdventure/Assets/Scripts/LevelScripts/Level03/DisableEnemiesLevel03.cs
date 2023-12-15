using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableEnemiesLevel03 : MonoBehaviour
{
    public static DisableEnemiesLevel03 disableEnemiesLevel03;
    public bool canDisableEnemies;
    private EnableObjects enableObjects;
    private bool canDisableDoor = true;
    private bool canDisableDialogue = false;
    private bool finishEvent = false;
    private GameObject door;
    private GameObject dialogue;

    private void Awake()
    {
        if (DisableEnemiesLevel03.disableEnemiesLevel03 == null)
        {
            DisableEnemiesLevel03.disableEnemiesLevel03 = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        enableObjects = GameObject.Find("LevelEvents").GetComponentInChildren<EnableObjects>();
        int totalDisabledEnemies = 0;

        if (SceneManager.GetActiveScene().name == "Level03B")
        {
            for (int i = 0; i < enableObjects.enemies.Length; i++)
            {
                if (!enableObjects.enemies[i].activeInHierarchy)
                {
                    totalDisabledEnemies++;
                }
            }

            if (totalDisabledEnemies == enableObjects.enemies.Length)
            {
                canDisableEnemies = true;
                canDisableDoor = false;
                canDisableDialogue = true;
                finishEvent = true;
            }

            DisableEnemies();
        }

        if (SceneManager.GetActiveScene().name != "Level03A" && SceneManager.GetActiveScene().name != "Level03B")
        {
            Destroy(gameObject);
        }
    }

    private void DisableEnemies()
    {
        if (canDisableDoor)
        {
            door = GameObject.Find("Door");

            if (door != null)
            {
                door.SetActive(false);
            }
        }

        if (canDisableDialogue)
        {
            dialogue = GameObject.Find("DialogueController").transform.GetChild(0).gameObject;

            if (dialogue != null)
            {
                dialogue.SetActive(false);
            }
        }

        if (canDisableEnemies)
        {
            for (int i = 0; i < enableObjects.enemies.Length; i++)
            {
                enableObjects.enemies[i].SetActive(false);
            }
        }

        if (!finishEvent)
        {
            enableObjects.canEnable = true;
        }
    }
}

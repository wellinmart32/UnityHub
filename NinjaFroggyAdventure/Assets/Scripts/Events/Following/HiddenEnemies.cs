using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenEnemies : MonoBehaviour
{
    private GameObject[] enemies;
    private Chronometer chronometer;
    [SerializeField] private float switchTime;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        chronometer = GameObject.Find("Chronometer").GetComponent<Chronometer>();
        enemies = new GameObject[transform.childCount];
        
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = transform.GetChild(i).gameObject;
        }

        DisableEnemies();
    }

    private void Update()
    {
        if (chronometer.time <= 0.01f && chronometer.time >= -1 && !chronometer.clockwise)
        {
            EnableEnemies();
        }
        else if (chronometer.time - 0.01f <= switchTime && chronometer.time + 1 >= switchTime && chronometer.clockwise)
        {
            DisableEnemies();
        }

        if (playerInfo.currentHearts <= 0)
        {
            DisableEnemies();
        }
    }

    private void EnableEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemies[i].GetComponent<RespawnEnemy>().RestoreEnemy();
        }
    }

    private void DisableEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(false);
        }
    }
}

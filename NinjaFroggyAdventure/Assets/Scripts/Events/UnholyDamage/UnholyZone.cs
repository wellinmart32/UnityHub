using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnholyZone : MonoBehaviour
{
    public int damage = 1;
    public float startTime = 2;
    private float deathTime;
    private bool isInside;
    private GameObject player;

    private void Awake()
    {
        deathTime = startTime;
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
    }

    private void Update()
    {
        if (isInside)
        {
            deathTime -= Time.deltaTime;
        }

        if (deathTime < 0)
        {
            player.GetComponent<PlayerRespawn>().PlayerDamaged(damage, false);
            deathTime = startTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInside = false;
            deathTime = startTime;
        }
    }
}

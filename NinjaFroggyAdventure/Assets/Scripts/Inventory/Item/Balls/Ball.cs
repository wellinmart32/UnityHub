using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject handBall;
    public GameObject thrownBall;
    public Transform launchSpawnPoint;
    public Item item;
    public AudioSource clip;
    private SpriteRenderer playerSP;

    void Awake()
    {
        GameObject player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        playerSP = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.Q)/* || Input.GetMouseButtonDown(0)*/)
            {
                LaunchBall();
            }

            if (playerSP.flipX == true)
            {
                handBall.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                handBall.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void LaunchBall()
    {
        GameObject ball;
        clip.Play();
        ball = Instantiate(thrownBall, launchSpawnPoint.position, launchSpawnPoint.rotation);
        Slot slot = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>().slots[item.id].GetComponent<Slot>();
        item.DecreaseDurability(slot);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenDoorLevel01 : MonoBehaviour
{
    private bool inDoor = false;
    private float doorTime = 1;
    private float startTime = 1;
    private GameObject player;
    private TransitionImage transitionImage;
    private GameDataManager gameDataManager;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        transitionImage = GameObject.Find("StandarInterface").GetComponent<Initialization>().transitionImage;
        gameDataManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().gameDataManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inDoor = false;
        doorTime = startTime;
    }

    private void Update()
    {
        if (inDoor)
        {
            doorTime -= Time.deltaTime;
        }

        if (doorTime < 0)
        {
            ChangeInstance();
        }
    }

    private void ChangeInstance()
    {
        if (gameObject.name == "Door05" || gameObject.name == "Door06" || gameObject.name == "Door10" || gameObject.name == "Door11" || gameObject.name == "Door12")
        {
            GameObject.Find("Chronometer").GetComponent<Chronometer>().FinishCount();

            for (int i = 0; i < GameObject.Find("Chronometer").transform.childCount - 1; i++)
            {
                GameObject.Find("Chronometer").transform.GetChild(i).GetComponent<ChronometerWatcher>().enableTimer = true;

            }
        }
            
        transitionImage.StartTransition(0f, 1, 0.3f);
        StartCoroutine(ChangePlayerPosition());
    }

    private IEnumerator ChangePlayerPosition()
    {
        yield return new WaitForSeconds(2);

        if (gameObject.name == "Door01")
        {
            player.transform.position = new Vector2(15.953f, -6.07037f);
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (gameObject.name == "Door02")
        {
            player.transform.position = new Vector2(58.61f, -7.83f);
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (gameObject.name == "Door03")
        {
            player.transform.position = new Vector2(26.8f, -2.390349f);
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (gameObject.name == "Door04")
        {
            player.transform.position = new Vector2(-8.366537f, -5.75036f);
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (gameObject.name == "Door05")
        {
            player.transform.position = new Vector2(56.32f, -20.94f);
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (gameObject.name == "Door06")
        { 
            player.transform.position = new Vector2(49.01001f, -5.590343f);
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (gameObject.name == "Door07")
        {
            player.transform.position = new Vector2(40.783f, -12.753f);
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (gameObject.name == "Door08")
        {
            player.transform.position = new Vector2(-9.87f, 0.3296787f);
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (gameObject.name == "Door09")
        {
            player.transform.position = new Vector2(38.71347f, -24.31033f);
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (gameObject.name == "Door10" || gameObject.name == "Door11" || gameObject.name == "Door12")
        {
            gameDataManager.SaveData();
            SceneManager.LoadScene("Level06B");
        }

        if (gameObject.name == "Door13")
        {
            player.transform.position = new Vector2(62.38f, -5.266544f);
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (gameObject.name == "Door14")
        {
            SceneManager.LoadScene("Level06A");
        }

        transitionImage.StartTransition(0f, 1, 0.3f);
    }
}

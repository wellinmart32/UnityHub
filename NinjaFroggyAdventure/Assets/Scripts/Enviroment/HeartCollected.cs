using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartCollected : MonoBehaviour
{
    public AudioSource clip;
    private GameObject player;
    public bool isCollected = false;
    public bool canPickUp = false;
    public bool canRestore;
    public float disableTime = 0.5f;
    public bool inChest;
    private BoxCollider2D coll2D;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        clip = GetComponent<AudioSource>();
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        coll2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {   
        if (canPickUp)
        {
            PickUpHeart();
            coll2D.enabled = false;
            canPickUp = false;
            StartCoroutine(DisableHeart());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            canPickUp = true;
            HideHeart();
            clip.Play();
        }
    }

    private void PickUpHeart()
    {
        if (playerInfo.currentActiveHearts < playerInfo.maxHearts)
        {
            playerInfo.currentActiveHearts++;
            playerInfo.currentHearts = playerInfo.currentActiveHearts;
            playerInfo.SetActiveHearts();
        }
        else
        {
            playerInfo.lifes += 3;
        }

        player.GetComponent<RecoveryPlayer>().RecoveryByHeart();
    }

    public void RestoreHeart()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        coll2D.enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        isCollected = false;
        gameObject.SetActive(true);
    }

    public void HideHeart()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        isCollected = true;
    }

    private IEnumerator DisableHeart()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}

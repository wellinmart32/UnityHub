using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCollected : MonoBehaviour
{
    public AudioSource clip;
    public bool isCollected = false;
    public bool canPickUp = false;
    public bool canRestore;
    public float disableTime = 0.5f;
    public bool inChest;
    private CircleCollider2D coll2D;
    private PlayerInfo playerInfo;
    private GameDataManager gameDataManager;

    private void Awake()
    {
        clip = GameObject.Find("LifePickUp").GetComponent<AudioSource>();
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        gameDataManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().gameDataManager;
        coll2D = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (canPickUp)
        {
            PickUpLife();
            coll2D.enabled = false;
            canPickUp = false;
            StartCoroutine(DisableLife());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            canPickUp = true;
            HideLife();
            clip.Play();
        }
    }

    private void PickUpLife()
    {
        playerInfo.lifes += 1;
        gameDataManager.SaveLifeData();
    }

    public void RestoreLife()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        coll2D.enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        isCollected = false;
        gameObject.SetActive(true);
    }

    public void HideLife()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        isCollected = true;
    }

    private IEnumerator DisableLife()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}

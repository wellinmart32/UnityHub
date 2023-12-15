using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemCollected : MonoBehaviour
{
    public AudioSource clip;
    public bool isCollected = false;
    public bool canPickUp = false;
    public bool canRestore;
    public float disableTime = 0.5f;
    public bool inChest;
    private BoxCollider2D coll2D;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        clip = GameObject.Find("GemPickUp").GetComponent<AudioSource>();
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        coll2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (canPickUp)
        {
            PickUpGem();
            coll2D.enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            canPickUp = false;
            StartCoroutine(DisableGem());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            canPickUp = true;
            HideGem();
            clip.Play();
        }
    }

    private void PickUpGem()
    {
        playerInfo.gems += 1;
    }

    public void RestoreGem()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        coll2D.enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        isCollected = false;
        gameObject.SetActive(true);
    }

    public void HideGem()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        isCollected = true;
    }

    private IEnumerator DisableGem()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}

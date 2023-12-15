using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BriefCollected : MonoBehaviour
{
    public AudioSource clip;
    public int value;
    public enum Fruit { Brief1, Brief2, Brief3 };
    public Fruit type;
    public bool isCollected = false;
    public bool canPickUp = false;
    public bool canRestore;
    public float disableTime = 0.5f;
    public bool inBox;
    public bool inChest;
    private BoxCollider2D coll2D;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        switch (type)
        {
            case Fruit.Brief1:
                value = 1;
                break;
            case Fruit.Brief2:
                value = 5;
                break;
            case Fruit.Brief3:
                value = 10;
                break;
            default:
                break;
        }

        clip = GameObject.Find("BriefPickUp").GetComponent<AudioSource>();
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        coll2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (canPickUp)
        {
            PickUpBrief();
            coll2D.enabled = false;
            canPickUp = false;
            StartCoroutine(DisableBrief());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            canPickUp = true;
            HideBrief();
            clip.Play();
        }
    }

    private void PickUpBrief()
    {
        playerInfo.briefs += value;
    }

    public void RestoreBrief()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        coll2D.enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        isCollected = false;
        gameObject.SetActive(true);
    }

    public void HideBrief()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        isCollected = true;
    }

    private IEnumerator DisableBrief()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}

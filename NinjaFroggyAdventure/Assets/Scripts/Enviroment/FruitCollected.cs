using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FruitCollected : MonoBehaviour
{
    public AudioSource clip;
    public int value;
    public enum Fruit { Apple, Banana, Cherry, Kiwi, Melon, Orange, Pineapple, Strawberry };
    public Fruit type;
    public bool isCollected = false;
    public bool canPickUp = false;
    public bool canRestore;
    public float disableTime = 0.5f;
    public bool inChest;
    private BoxCollider2D coll2D;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        switch (type)
        {
            case Fruit.Apple:
                value = 10;
                break;
            case Fruit.Banana:
                value = 50;
                break;
            case Fruit.Cherry:
                value = 2;
                break;
            case Fruit.Kiwi:
                value = 5;
                break;
            case Fruit.Melon:
                value = 200;
                break;
            case Fruit.Orange:
                value = 25;
                break;
            case Fruit.Pineapple:
                value = 100;
                break;
            case Fruit.Strawberry:
                value = 1;
                break;
            default:
                break;
        }

        if (value == 1 || value == 2 || value == 5)
        {
            clip = GameObject.Find("FruitPickUp").GetComponent<AudioSource>();
        }
        else if (value == 10 || value == 25 || value == 50)
        {
            clip = GameObject.Find("FruitPickUp (1)").GetComponent<AudioSource>();
        }
        else if (value == 100 || value == 200)
        {
            clip = GameObject.Find("FruitPickUp (2)").GetComponent<AudioSource>();
        }

        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
        coll2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (canPickUp)
        {
            PickUpFruit();
            coll2D.enabled = false;
            canPickUp = false;
            StartCoroutine(DisableFruit());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            canPickUp = true;
            HideFruit();
            clip.Play();
        }
    }

    private void PickUpFruit()
    {
        playerInfo.fruits += value;
    }

    public void RestoreFruit()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        coll2D.enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        isCollected = false;
        gameObject.SetActive(true);
    }

    public void HideFruit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        isCollected = true;
    }

    private IEnumerator DisableFruit()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}

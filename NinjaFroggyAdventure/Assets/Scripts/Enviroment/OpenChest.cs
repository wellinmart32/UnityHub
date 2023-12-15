using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Animator animator;
    public bool inChest = false;
    public int totalTreasures = 3;
    public GameObject[] treasures;
    public AudioSource[] clips;
    private float soundTime;
    private float chestTime = 3;
    private float startTime = 3;
    private bool canListenOpen;
    private bool canListenClose;

    private void Awake()
    {
        for (int i = 0; i < treasures.Length; i++)
        {
            treasures[i].SetActive(false);

            if (treasures[i].CompareTag("Fruit"))
            {
                treasures[i].transform.SetParent(GameObject.Find("FruitManager").transform);
            }
            else if (treasures[i].CompareTag("Gem"))
            {
                treasures[i].transform.SetParent(GameObject.Find("GemManager").transform);
            }
            else if (treasures[i].CompareTag("Brief"))
            {
                treasures[i].transform.SetParent(GameObject.Find("BriefManager").transform);
            }
            else if (treasures[i].CompareTag("Life"))
            {
                treasures[i].transform.SetParent(GameObject.Find("LifeManager").transform);
            }
            else if (treasures[i].CompareTag("Heart"))
            {
                treasures[i].transform.SetParent(GameObject.Find("HeartManager").transform);
            }
        }

        clips = new AudioSource[2];
        clips[0] = GameObject.Find("OpenChest").GetComponent<AudioSource>();
        clips[1] = GameObject.Find("CloseChest").GetComponent<AudioSource>();
        soundTime = 0.2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inChest = false;
            canListenClose = true;
            chestTime = startTime;
            animator.SetBool("Open", false);
            clips[1].Play();
            //CloseChestSound();
        }
    }

    private void Update()
    {
        if (inChest)
        {
            chestTime -= Time.deltaTime;
        }

        if (chestTime <= 0 && chestTime >= -3)
        {
            ActiveChest();
            chestTime -= 5;
            canListenOpen = true;
            
            clips[0].Play();
        }

        //if (canListenOpen)
        //{
        //    OpenChestSound();
        //}

        //if (canListenClose)
        //{
        //    CloseChestSound();
        //}
    }

    public void ActiveChest()
    {
        animator.SetBool("Open", true);

        for (int i = 0; i < treasures.Length; i++)
        {
            treasures[i].SetActive(true);
        }
    }

    //private void OpenChestSound()
    //{
    //    if (!clips[0].isPlaying && soundTime > 0)
    //    {
    //        clips[0].Play();
    //    }

    //    soundTime -= Time.deltaTime;

    //    if (soundTime <= 0)
    //    {
    //        clips[0].Stop();
    //        clips[1].time = 0.7f;
    //        soundTime = 0.6f;
    //        canListenOpen = false;
    //    }
    //}

    //private void CloseChestSound()
    //{
    //    if (!clips[1].isPlaying && soundTime > 0)
    //    {
    //        clips[1].Play();
    //    }

    //    soundTime -= Time.deltaTime;
        
    //    if (soundTime <= 0)
    //    {
    //        clips[1].Stop();
    //        soundTime = 0.2f;
    //        canListenClose = false;
    //    }
    //}
}

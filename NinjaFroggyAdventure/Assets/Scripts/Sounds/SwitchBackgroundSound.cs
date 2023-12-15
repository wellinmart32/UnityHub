using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBackgroundSound : MonoBehaviour
{
    public int listNumber;
    private BackgroundSound backgroundSound;
    private void Awake()
    {
        backgroundSound = GameObject.Find("StandarInterface").GetComponent<Initialization>().backgroundSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            backgroundSound.listNumber = listNumber;
        }
    }
}

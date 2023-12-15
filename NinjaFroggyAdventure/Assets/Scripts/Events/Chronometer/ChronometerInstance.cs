using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronometerInstance : MonoBehaviour
{
    public bool isInside;
    private ClockSounds clockSounds;

    private void Awake()
    {
        clockSounds = GameObject.Find("ClockSounds").GetComponent<ClockSounds>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = true;
            clockSounds.canPlaySound = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = false;
            clockSounds.canPlaySound = false;
        }
    }
}

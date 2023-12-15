using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronometerCycle : MonoBehaviour
{
    public float switchTime;
    public int[] backgroundSoundnumbers;
    private Chronometer chronometer;
    private BackgroundSound backgroundSound;
    private void Awake()
    {
        chronometer = GetComponentInParent<Chronometer>();
        backgroundSound = GameObject.Find("StandarInterface").GetComponent<Initialization>().backgroundSound;
    }

    private void Update()
    {
        if (chronometer.time <= 0 && !chronometer.countText.gameObject.activeInHierarchy)
        {
            chronometer.clockwise = true;
            chronometer.countText.gameObject.SetActive(true);

            if (backgroundSoundnumbers != null)
            {
                backgroundSound.listNumber = backgroundSoundnumbers[1];
            }
        }
        else if (chronometer.time >= switchTime && chronometer.clockwise)
        {
            chronometer.time = chronometer.startTime;
            chronometer.clockwise = false;
            
            if (backgroundSoundnumbers != null)
            {
                backgroundSound.listNumber = backgroundSoundnumbers[0];
            }
        }
    }
}

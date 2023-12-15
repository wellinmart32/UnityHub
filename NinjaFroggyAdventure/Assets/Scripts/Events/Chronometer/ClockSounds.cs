using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockSounds : MonoBehaviour
{
    public AudioSource[] clips;
    public bool canPlaySound;
    public Chronometer chronometer;

    private void Update()
    {
        PlayClockSounds();
    }

    private void PlayClockSounds()
    {
        if (canPlaySound)
        {
            float time = chronometer.time;

            if (time > 30 && !chronometer.clockwise)
            {
                if (!clips[0].isPlaying)
                {
                    clips[0].Play();
                }
            }
            else if (time <= 30 && time >= 1 && !chronometer.clockwise)
            {
                if (clips[0].isPlaying)
                {
                    clips[0].Stop();
                }

                if (!clips[1].isPlaying)
                {
                    clips[1].Play();
                }
            }
            else if (time <= 0)
            {
                if (clips[0].isPlaying)
                {
                    clips[0].Stop();
                }

                if (clips[1].isPlaying)
                {
                    clips[1].Stop();
                }
            }
        }
        else
        {
            if (clips[0].isPlaying)
            {
                clips[0].Stop();
            }

            if (clips[1].isPlaying)
            {
                clips[1].Stop();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public bool canListen = true;
    public float waitTime = 3f;
    public int listNumber = 0;
    public float minVolume;
    public float maxVolume;
    [SerializeField] private float switchSoundFrecuency = 0.001f;
    private AudioSource[] soundList;
    private bool canDecreaseVolume;
    private float soundTime;
    private int previousListNumber = 0;
    private int i = 0;

    private void Awake()
    {
        SetSoundList(listNumber);
    }

    private void Update()
    {
        SwitchSoundList();
        SwitchBackgroundSound();
        SwitchSoundVolume();
    }

    private void SwitchSoundList()
    {
        if (previousListNumber != listNumber)
        {
            if (soundList[i].volume == 0)
            {
                SetSoundList(listNumber);
                previousListNumber = listNumber;
            }
            else
            {
                canDecreaseVolume = true;
            }
        }
        else
        {
            canDecreaseVolume = false;
        }
    }

    private void SwitchBackgroundSound()
    {
        if (canListen)
        {
            soundTime -= Time.deltaTime;

            if (soundTime <= 0)
            {
                if (soundList.Length == i + 1)
                {
                    i = 0;
                }
                else
                {
                    soundList[i].Stop();
                    i++;
                }

                soundTime = soundList[i].clip.length + waitTime;
                soundList[i].Play();
            }
            else
            {
                if (!soundList[i].isPlaying && soundTime - 3 > waitTime)
                {
                    soundList[i].Play();
                }
            }
        }
        else
        {
            PauseCurrentSound();
        }
    }

    private void SwitchSoundVolume()
    {
        if (!canDecreaseVolume)
        {
            IncreaseSoundVolume();
        }
        else
        {
            DecreaseSoundVolume();
        }
    }

    private void DecreaseSoundVolume()
    {
        if (soundList[i].volume - switchSoundFrecuency > minVolume)
        {
            soundList[i].volume -= switchSoundFrecuency;
        }
        else
        {
            soundList[i].volume = minVolume;
        }
    }

    private void IncreaseSoundVolume()
    {
        if (soundList[i].volume + switchSoundFrecuency < maxVolume)
        {
            soundList[i].volume += switchSoundFrecuency;
        }
        else
        {
            soundList[i].volume = maxVolume;
        }
    }

    private void PauseCurrentSound()
    {
        if (soundList != null)
        {
            if (soundList[i].isPlaying)
            {
                soundList[i].Pause();
            }
        }
    }

    private void StopCurrentSound()
    {
        if (soundList != null)
        {
            if (soundList[i].isPlaying)
            {
                soundList[i].Stop();
            }
        }
    }

    public void SetSoundList(int listPosition)
    {
        StopCurrentSound();
        GameObject soundController = gameObject.transform.GetChild(listPosition).gameObject;
        int soundListLength = soundController.transform.childCount;
        soundList = new AudioSource[soundListLength];

        for (int i = 0; i < soundListLength; i++)
        {
            soundList[i] = soundController.transform.GetChild(i).GetComponent<AudioSource>();
        }

        i = soundList.Length - 1;
        soundTime = 0;
    }
}

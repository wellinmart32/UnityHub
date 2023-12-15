using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateElementSound : MonoBehaviour
{
    public enum SoundElementType { LanceTrap };
    public SoundElementType type;
    public float waitTime = 0.1f;
    private AudioSource clip;

    private void Awake()
    {
        if (type == SoundElementType.LanceTrap)
        {
            clip = GameObject.Find("ActivateSpearTrap").GetComponent<AudioSource>();
        }

        clip = GetComponent<AudioSource>();
        waitTime += clip.clip.length;
        PlayAndDestroy();
    }

    public void PlayAndDestroy()
    {
        clip.Play();
        Destroy(gameObject, waitTime);
    }
}

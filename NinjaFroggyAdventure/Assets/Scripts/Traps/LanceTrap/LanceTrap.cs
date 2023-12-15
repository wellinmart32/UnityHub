using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceTrap : MonoBehaviour
{
    public float speed = 4f;
    public Transform[] moveSpots;
    public float startWaitTime = 3;
    public bool isInversive;
    private float waitTime;
    private int i = 0;
    public AISounds aISounds;
    private float startSoundTime;
    private float waitSoundTime;
    private AudioSource clip;

    private void Awake()
    {
        clip = GetComponent<AudioSource>();
        aISounds = transform.parent.parent.GetComponent<AISounds>();
        waitTime = startWaitTime;
        waitSoundTime = 0;

        if (isInversive)
        {
            i = moveSpots.Length - 1;
            transform.position = moveSpots[i].transform.position;
        }
    }

    private void Start()
    {
        if (aISounds)
        {
            startSoundTime = aISounds.aISoundWatchers1[0].clip.clip.length + aISounds.waitTime;
        }
    }

    private void Update()
    {
        StopSound();
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }

                waitTime = startWaitTime;
                PlaySound();

                if (clip)
                {
                    clip.Play();
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void StopSound()
    {
        if (aISounds)
        {
            if (waitSoundTime <= 0)
            {

                aISounds.canListen = false;
            }
            else
            {
                waitSoundTime -= Time.deltaTime;
            }
        }
    }

    private void PlaySound()
    {
        if (aISounds)
        {
            waitSoundTime = startSoundTime;
            aISounds.canListen = true;
        }
    }
}

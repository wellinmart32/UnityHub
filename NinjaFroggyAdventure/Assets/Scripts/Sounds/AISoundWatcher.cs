using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISoundWatcher : MonoBehaviour
{
    public AudioSource clip;
    public bool isUsing;
    public Transform startParent;

    private void Awake()
    {
        clip = GetComponent<AudioSource>();
        isUsing = false;
        startParent = transform.parent;
    }

    public void FreeSound()
    {
        isUsing = false;
        clip.Stop();
        transform.parent = startParent;
        transform.position = startParent.position;
    }
}

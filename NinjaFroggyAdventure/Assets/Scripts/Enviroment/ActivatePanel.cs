using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePanel : MonoBehaviour
{
    public bool order;
    public GameObject bridgePart;
    public Animator animator;
    public AudioSource clip;
    public float startWaitTime = 5;
    private float waitTime;
    private bool inDevice = false;

    private void Awake()
    {
        waitTime = startWaitTime;
        animator = GetComponent<Animator>();

        if (bridgePart)
        {
            bridgePart.GetComponent<Animator>().SetBool("Activate", !order);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inDevice = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inDevice = false;
        waitTime = startWaitTime;
    }

    void Update()
    {
        if (inDevice)
        {
            waitTime -= Time.deltaTime;
        }

        if (waitTime < 0)
        {
            SwitchVisibility();
        }
    }

    public void SwitchVisibility()
    {
        if (clip)
        {
            clip.Play();
        }

        animator.SetBool("Activate", true);

        if (bridgePart != null)
        {
            bridgePart.GetComponent<Animator>().SetBool("Activate", order);
            bridgePart.GetComponent<BoxCollider2D>().enabled = order;
        }
    }
}

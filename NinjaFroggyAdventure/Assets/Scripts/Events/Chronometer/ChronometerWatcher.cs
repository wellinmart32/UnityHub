using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronometerWatcher : MonoBehaviour
{
    public Chronometer chronometer;
    public bool enableTimer = true;

    private void Start()
    {
        chronometer = GetComponentInParent<Chronometer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (enableTimer)
            {
                chronometer.StartCount();
                
                for (int i = 0; i < gameObject.transform.parent.childCount; i++)
                {
                    if (gameObject.transform.parent.transform.GetChild(i).GetComponent<ChronometerWatcher>() != null)
                    {
                        gameObject.transform.parent.transform.GetChild(i).GetComponent<ChronometerWatcher>().enableTimer = false;
                    }
                }
            }
        }
    }
}

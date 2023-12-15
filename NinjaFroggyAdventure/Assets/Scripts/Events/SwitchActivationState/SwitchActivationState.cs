using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivationState : MonoBehaviour
{
    public float startTime = 1.5f;
    public bool activateState;
    public GameObject element;
    private float waitTime;
    private bool canEnable = false;

    private void Awake()
    {
        waitTime = startTime;
    }

    private void Update()
    {
        if (canEnable)
        {
            waitTime -= Time.deltaTime;
        }

        if (waitTime < 0)
        {
            element.SetActive(activateState);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canEnable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canEnable = false;
            waitTime = startTime;
        }
    }
}

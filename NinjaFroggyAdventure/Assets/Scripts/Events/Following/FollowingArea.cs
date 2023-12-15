using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingArea : MonoBehaviour
{
    public bool canFollow;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canFollow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canFollow = false;
        }
    }
}

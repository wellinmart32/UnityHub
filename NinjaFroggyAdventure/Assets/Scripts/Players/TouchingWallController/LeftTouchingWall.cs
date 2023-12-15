using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTouchingWall : MonoBehaviour
{
    public static bool isTouchingWall;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingWall = false;
        }
    }
}

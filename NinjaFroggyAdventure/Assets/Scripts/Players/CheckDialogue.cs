using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDialogue : MonoBehaviour
{
    public static bool exisDialogue;
    public static Dialogue dialogue;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Dialogue"))
        {
            exisDialogue = true;
            SetDialogue(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dialogue"))
        {
            exisDialogue = false;
        }
    }

    private void SetDialogue(Collider2D collision)
    {
        dialogue = collision.gameObject.GetComponent<Dialogue>();
    }
}

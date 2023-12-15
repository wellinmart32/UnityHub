using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool isUniqueDialogue;
    public MoveAndDisappear moveAndDisappear;
    public Dialogue dialogue;
    public float waitTime = 0.5f;
    private bool canStartDialogue = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //StartDialogueInteraction();
            StartCoroutine(DisappearNPC());
        }
    }

    private void StartDialogueInteraction()
    {
        if (canStartDialogue)
        {
            dialogue.enableDialogue = true;

            if (moveAndDisappear != null)
            {
                StartCoroutine(DisappearNPC());
            }
        }

        if (isUniqueDialogue)
        {
            canStartDialogue = false;
        }
    }

    private IEnumerator DisappearNPC()
    {
        yield return new WaitForSeconds(waitTime);
        moveAndDisappear.canMove = true;
    }
}

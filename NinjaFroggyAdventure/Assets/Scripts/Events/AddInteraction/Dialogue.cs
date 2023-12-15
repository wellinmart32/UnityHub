using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public bool isUnique;
    public bool isAutomatic;

    // Estas variables no deben alterarse desde unity en tiempo de ejecucion
    public bool enableDialogue;
    public bool canDialogueAgain = true;
    public bool isPlayerInRange;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;
    private TimeController timeController;
    private Inventory inventory;
    private DialogueInterface dialogueInterface;

    private void Awake()
    {
        dialogueInterface = GameObject.Find("StandarInterface").GetComponent<Initialization>().dialogueInterface;
        timeController = GameObject.Find("StandarInterface").GetComponent<Initialization>().timeController;
        inventory = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (isAutomatic && !didDialogueStart && canDialogueAgain && isPlayerInRange)
        {
            dialogueInterface.ShowDialogueInterface();
            dialogueInterface.showDialogueButton.SetActive(false);
            StartDialogue();
        }

        if (isPlayerInRange && enableDialogue && canDialogueAgain)
        {
            enableDialogue = false;

            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            DialogueInterface.dialogue = this;

            if (canDialogueAgain)
            {
                dialogueInterface.showDialogueButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            DialogueInterface.dialogue = null;
            dialogueInterface.showDialogueButton.SetActive(false);

            if (!isUnique)
            {
                canDialogueAgain = true;
            }
        }
    }

    public void StartDialogue()
    {
        inventory.canShow = false;
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        timeController.StopTime();
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueInterface.HideDialogueInterface();
            timeController.RestoreTime();
            inventory.canShow = true;
            CheckDialogue.dialogue = null;
            canDialogueAgain = false;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char c in dialogueLines[lineIndex])
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
}

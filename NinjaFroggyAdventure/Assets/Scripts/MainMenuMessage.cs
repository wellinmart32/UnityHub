using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuMessage : MonoBehaviour
{
    public Text message;
    public Text confirmText;
    public Text remainTimeText;
    public GameObject container;
    public MainMenu mainMenu;
    public AudioSource[] buttonClips;
    public GameObject confirmButton;
    public GameObject cancelButton;
    public int selectedOption;
    public float[] confirmButtonPositions;
    public bool isImportant;
    public float startTime = 5;
    private float buttonTime;
    private Color enableColor;
    private Color disableColor;

    private void Awake()
    {
        Time.timeScale = 1;
        buttonTime = startTime;
        enableColor = confirmButton.GetComponent<Image>().color;
        disableColor = Color.gray;
    }

    private void Update()
    {
        ButtonLockTime();
    }

    public void Show(string message, bool onlyInfo)
    {
        if (onlyInfo)
        {
            confirmButton.GetComponent<RectTransform>().localPosition = new Vector2(confirmButtonPositions[0], confirmButton.transform.localPosition.y);
            cancelButton.SetActive(false);
        }
        else
        {
            if (isImportant)
            {
                LockButton();
            }

            confirmButton.GetComponent<RectTransform>().localPosition = new Vector2(confirmButtonPositions[1], confirmButton.transform.localPosition.y);
            cancelButton.SetActive(true);
        }

        this.message.text = message;
        container.SetActive(true);
    }

    public void Confirm()
    {
        buttonClips[0].Play();

        if (selectedOption == 1)
        {
            mainMenu.NewGame();
        }

        container.SetActive(false);
    }

    public void Cancel()
    {
        buttonClips[2].Play();
        container.SetActive(false);
    }

    private void ButtonLockTime()
    {
        if (isImportant)
        {
            CalculateTime();

            if (buttonTime <= 0)
            {
                UnlockButton();
            }
        }
    }

    private void CalculateTime()
    {
        buttonTime -= Time.deltaTime;
        int minutes = (int)buttonTime / 60;
        int seconds = (int)buttonTime % 60;
        remainTimeText.text = minutes.ToString() + ":" + seconds.ToString().PadLeft(2, '0');
    }


    private void LockButton()
    {
        confirmButton.GetComponent<Image>().color = disableColor;
        confirmButton.GetComponent<Button>().enabled = false;
        remainTimeText.gameObject.SetActive(true);
        confirmText.gameObject.SetActive(false);
    }

    private void UnlockButton()
    {
        buttonTime = startTime;
        isImportant = false;
        confirmButton.GetComponent<Image>().color = enableColor;
        remainTimeText.gameObject.SetActive(false);
        confirmText.gameObject.SetActive(true);
        confirmButton.GetComponent<Button>().enabled = true;
    }
}

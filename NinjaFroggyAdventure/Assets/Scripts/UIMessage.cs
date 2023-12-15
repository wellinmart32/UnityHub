using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
    public Text message;
    public GameObject container;
    public AudioSource[] buttonClips;
    public GameObject confirmButton;
    public GameObject cancelButton;
    public int selectedOption;
    public enum UIMessageType { MainMenu, Options };
    public UIMessageType uIMessageType;
    public float[] confirmButtonPositions;

    public void Show(string message, bool onlyInfo)
    {
        if (onlyInfo)
        {
            confirmButton.GetComponent<RectTransform>().localPosition = new Vector2(confirmButtonPositions[0], confirmButton.transform.localPosition.y);
            cancelButton.SetActive(false);
        }
        else
        {
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
            switch (uIMessageType)
            {
                case UIMessageType.MainMenu:
                    MainMenu mainMenu = GameObject.Find("").GetComponent<MainMenu>();
                    mainMenu.NewGame();
                    break;
                case UIMessageType.Options:
                    OptionsManager optionsManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().optionsManager;
                    optionsManager.MainMenu();
                    break;
                default:
                    break;
            }
        }

        container.SetActive(false);
    }

    public void Cancel()
    {
        buttonClips[2].Play();
        container.SetActive(false);
    }
}

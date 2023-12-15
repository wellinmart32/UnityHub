using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public GameObject mobileControls;
    public GameObject inventoryButton;
    public GameObject optionsPanel;
    public GameObject optionsButton;
    public UIMessage uIMessage;
    [SerializeField, TextArea(4, 6)] private string[] messages;
    public AudioSource clip;
    public float waitTime;
    private TimeController timeController;

    private void Awake()
    {
        timeController = GameObject.Find("StandarInterface").GetComponent<Initialization>().timeController;
    }

    public void OptionsPanel()
    {
        clip.Play();
        mobileControls.SetActive(false);
        inventoryButton.SetActive(false);
        optionsButton.SetActive(false);
        timeController.StopTime();
        optionsPanel.SetActive(true);
    }

    public void Return()
    {
        clip.Play();
        mobileControls.SetActive(true);
        inventoryButton.SetActive(true);
        optionsButton.SetActive(true);
        timeController.RestoreTime();
        optionsPanel.SetActive(false);
    }
    
    public void Settings()
    {
        clip.Play();
    }

    public void ConfirmMainMenu()
    {
        clip.Play();
        uIMessage.selectedOption = 1;
        uIMessage.Show(messages[0], false);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        clip.Play();
        StartCoroutine(QuitApplication());
    }

    private IEnumerator QuitApplication()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Application.Quit();
    }
}

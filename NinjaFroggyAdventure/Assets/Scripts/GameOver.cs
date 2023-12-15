using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Image transitionImage;
    public string route;

    private void Awake()
    {
        transitionImage.CrossFadeAlpha(255, 0, false);
        transitionImage.CrossFadeAlpha(1, 5, false);
        Invoke("OpenMainMenu", 5);
    }

    private void OpenMainMenu()
    {
        transitionImage.CrossFadeAlpha(255, 6, false);
        StartCoroutine(OpenRoute(route));
    }

    private IEnumerator OpenRoute(string route)
    {
        yield return new WaitForSecondsRealtime(9);
        SceneManager.LoadScene(route);
    }
}

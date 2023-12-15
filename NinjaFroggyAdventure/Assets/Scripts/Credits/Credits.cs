using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public AudioSource[] clips;
    public string route;
    public float waitTime;

    public void OpenMainMenu()
    {
        clips[1].Play();
        StartCoroutine(OpenRoute(route));
    }

    private IEnumerator OpenRoute(string route)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(route);
    }
}

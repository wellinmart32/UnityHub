using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string level;
    public float startTime = 1.5f;
    private float doorTime;
    private bool inDoor = false;
    private TransitionImage transitionImage;

    private void Awake()
    {
        doorTime = startTime;
        transitionImage = GameObject.Find("StandarInterface").GetComponent<Initialization>().transitionImage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoor = true;
        }
    }

    private void Update()
    {
        if (inDoor)
        {
            doorTime -= Time.deltaTime;
        }

        if (doorTime < 0)
        {
            ChangeInstance();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoor = false;
            doorTime = startTime;
        }
    }

    private void ChangeInstance()
    {
        GameObject.Find("TransitionImage").GetComponent<TransitionImage>().StartTransition(0.3f, 3, 0.3f);
        StartCoroutine(OpenLevel());
    }

    private IEnumerator OpenLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);

    }
}

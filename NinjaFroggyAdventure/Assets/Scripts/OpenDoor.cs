using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    public Vector2 teleportPosition;
    public bool playerFlip;
    public float startTime = 1;
    private float doorTime;
    private bool inDoor = false;
    private GameObject player;

    private void Awake()
    {
        doorTime = startTime;
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
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
        StartCoroutine(ChangePlayerPosition());
    }

    private IEnumerator ChangePlayerPosition()
    {
        yield return new WaitForSeconds(1);
        player.transform.position = teleportPosition;
        player.GetComponent<SpriteRenderer>().flipX = playerFlip;
    }
}

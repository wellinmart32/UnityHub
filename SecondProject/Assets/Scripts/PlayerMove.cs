using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayrMove : MonoBehaviour
{
    public bool canMove = false;
    public float speed = 0.5f;
    public GameObject player;
    private GameObject wayPoint;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    private float waitTime;
    private int i = 0;
    private Vector2 currentPosition;
    private void Awake()
    {
        wayPoint = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (canMove)
        {
            StartMove();
        }
    }

    public void StartMove()
    {
        //StartCoroutine(CheckMoving());
        player.transform.position = Vector2.MoveTowards(player.transform.position, wayPoint.transform.position, speed * Time.deltaTime);
    }

    private IEnumerator CheckMoving()
    {
        currentPosition = transform.position;
        yield return new WaitForSeconds(0.5f);

        if (transform.position.x > currentPosition.x)
        {
            if (spriteRenderer != null) spriteRenderer.flipX = true;
            if (animator != null) animator.SetBool("Idle", false);
        }
        else if (transform.position.x < currentPosition.x)
        {
            if (spriteRenderer != null) spriteRenderer.flipX = false;
            if (animator != null) animator.SetBool("Idle", false);
        }
        else if (transform.position.x == currentPosition.x)
        {
            if (animator != null) animator.SetBool("Idle", true);
        }
    }
}

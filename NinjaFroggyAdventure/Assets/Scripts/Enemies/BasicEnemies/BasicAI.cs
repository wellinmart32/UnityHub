using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    public bool canMove = true;
    public float speed = 0.5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    public float feintTime = 2f;
    private float waitTime;
    private int i = 0;
    private Vector2 currentPosition;
    
    void Start()
    {
        waitTime = startWaitTime;
    }

    void Update()
    {
        if (canMove)
        {
            StartCoroutine(CheckMoving());
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
            {
                if (waitTime <= 0)
                {
                    if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }

                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        else
        {
            if (animator != null) animator.SetBool("Idle", true);
        }
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

    public IEnumerator GotFeint()
    {
        canMove = false;
        if (animator != null) animator.SetBool("Idle", true);
        yield return new WaitForSeconds(feintTime);
        canMove = true;
        if (animator != null) animator.SetBool("Idle", false);
    }
}

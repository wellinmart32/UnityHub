using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDisappear : MonoBehaviour
{
    public float speed = 0.5f;
    public bool canMove;
    public bool inversiveFlip;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    private float waitTime;
    private int i = 0;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 currentPosition;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                    gameObject.SetActive(false);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    IEnumerator CheckMoving()
    {
        currentPosition = transform.position;
        yield return new WaitForSeconds(0);

        if (!inversiveFlip)
        {
            if (transform.position.x > currentPosition.x)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Idle", false);
            }
            else if (transform.position.x < currentPosition.x)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Idle", false);
            }
            else if (transform.position.x == currentPosition.x)
            {
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            if (transform.position.x > currentPosition.x)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Idle", false);
            }
            else if (transform.position.x < currentPosition.x)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Idle", false);
            }
            else if (transform.position.x == currentPosition.x)
            {
                animator.SetBool("Idle", true);
            }
        }
    }
}

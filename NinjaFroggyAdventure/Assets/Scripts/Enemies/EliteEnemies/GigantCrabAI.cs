using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigantCrabAI : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed = 0.5f;
    private float waitTime;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    private int i = 0;
    private Vector2 currentPosition;

    void Start()
    {
        waitTime = startWaitTime;
    }

    void Update()
    {
        StartCoroutine(CheckEnemyMoving());
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;

                    if (i % 2 != 0 || i == 0)
                    {
                        ChangeEnemyPosition();
                    }
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

    private void ChangeEnemyPosition()
    {
        transform.position = moveSpots[i].position;
    }

    IEnumerator CheckEnemyMoving()
    {
        currentPosition = transform.position;
        yield return new WaitForSeconds(0.5f);

        if (transform.position.x > currentPosition.x)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Walk", true);
        }
        else if (transform.position.x < currentPosition.x)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Walk", true);
        }
        else if (transform.position.x == currentPosition.x)
        {
            animator.SetBool("Walk", false);
        }
    }
}

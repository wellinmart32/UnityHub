using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAI : MonoBehaviour
{
    public bool canMove = true;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed = 0.75f;
    public float feintTime = 2f;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    private float waitTime;
    private int i = 0;

    private void Awake()
    {
        waitTime = startWaitTime;
    }

    private void Update()
    {
        if (canMove)
        {
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
    }

    public IEnumerator GotFeint()
    {
        canMove = false;
        yield return new WaitForSeconds(feintTime);
        canMove = true;
    }
}

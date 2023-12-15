using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed = 0.5f;
    public bool isAsymmetric;
    private bool isRegressing;
    public bool isInversive;
    public bool canMove = true;
    public bool isSystem = false;
    public bool isStart = true;
    private float waitTime;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    private int i = 0;

    void Awake()
    {
        waitTime = startWaitTime;

        if (isInversive)
        {
            i = moveSpots.Length - 1;
            isRegressing = true;
            transform.position = moveSpots[moveSpots.Length - 1].transform.position;
        }
    }

    void Update()
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
                        if (isAsymmetric)
                        {
                            if (isRegressing)
                            {
                                if (i == 0)
                                {
                                    if (!isInversive && isSystem)
                                    {
                                        canMove = false;
                                    }
                                    isRegressing = false;
                                }
                                else
                                {
                                    i--;
                                }
                            }
                            else
                            {
                                i++;
                            }
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {

                        if (isAsymmetric)
                        {
                            isRegressing = true;

                            if (moveSpots[i] == moveSpots[moveSpots.Length - 1])
                            {
                                if (isInversive && isSystem && !isStart)
                                {
                                    canMove = false;
                                }
                                i--;
                                isStart = false;
                            }
                        }
                        else
                        {
                            i = 0;
                        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureCollected : MonoBehaviour
{
    public Transform[] moveSpots;
    public float speed = 0.5f;
    public float startWaitTime = 2;
    public float waitTime = 2;
    private int i = 1;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) <= 0)
        {
            if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
            {
                i++;
            }
        }

        if (waitTime <= 0 && waitTime >= -3)
        {
            if (CompareTag("Life"))
            {
                GetComponent<CircleCollider2D>().enabled = true;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }

            waitTime -= 5;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}

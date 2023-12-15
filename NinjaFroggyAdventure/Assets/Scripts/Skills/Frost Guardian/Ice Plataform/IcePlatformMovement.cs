using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatformMovement : MonoBehaviour
{
    public float lifeTime;
    public float velocity = 0.25f;
    public float separation = 1.5f;
    private Vector2[] positionList;
    private Vector2 endPosition;
    private int totalPositions = 3;
    private bool canMove;
    private int count = 1;


    private void Awake()
    {
        InitDestroy();
        InitPositionList();
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }
        else
        {
            RestorePlatformPosition();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = false;
        }
    }

    private void Move()
    {
        endPosition = positionList[count];
        transform.position = Vector2.MoveTowards(transform.position, endPosition, velocity * Time.deltaTime);

        if (Vector2.Distance(transform.position, endPosition) < 0.1f)
        {
            if (positionList[count] != positionList[totalPositions - 1])
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }
    }

    private void RestorePlatformPosition()
    {
        count = 1;
        endPosition = positionList[count];
        transform.position = Vector2.MoveTowards(transform.position, endPosition, velocity * Time.deltaTime);
    }

    public void InitDestroy()
    {
        StartCoroutine(DestroyPlatform());
    }

    public void InitPositionList()
    {
        positionList = new Vector2[totalPositions];
        positionList[0] = new Vector2(transform.position.x, transform.position.y - separation);
        positionList[1] = new Vector2(transform.position.x, transform.position.y);
        positionList[2] = new Vector2(transform.position.x, transform.position.y + separation);
    }

    public IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }
}

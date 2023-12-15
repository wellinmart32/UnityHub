using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingSpikeBall : MonoBehaviour
{
    public float speed = 1.8f;
    public bool canMove;
    private float waitTime;
    private Animator animator;
    public float startWaitTime = 1;
    public Transform[] moveSpots;
    public GameObject enemyDamage;
    public SwitchSpikeBallState switchSpikeBallState;
    private int i = 0;
    private Vector2 currentPosition;
    private Vector2 parentStartPosition;
    private Vector2 startPosition;
    private Quaternion startRotation;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        waitTime = startWaitTime;
        animator = GetComponent<Animator>();
        parentStartPosition = transform.parent.position;
        startPosition = transform.position;
        startRotation = transform.rotation;
        playerInfo = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerInfo;
    }

    private void Update()
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
                        DisableSpikeBall();
                    }
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        if (playerInfo.currentHearts <= 0)
        {
            switchSpikeBallState.RestoreLock();
            StartCoroutine(RestoreSpikeBall());
        }
    }

    public void DisableSpikeBall()
    {
        GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        enemyDamage.GetComponent<DamageObject>().damage = 0;
    }

    private IEnumerator CheckMoving()
    {
        currentPosition = transform.position;
        yield return new WaitForSeconds(0.5f);

        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {
            animator.SetBool("LeftRotation", false);
            animator.SetBool("RightRotation", false);
        }
        else if (transform.position.x > currentPosition.x)
        {
            animator.SetBool("LeftRotation", false);
            animator.SetBool("RightRotation", true);
        }
        else if (transform.position.x < currentPosition.x)
        {
            animator.SetBool("LeftRotation", true);
            animator.SetBool("RightRotation", false);
        }
    }

    public IEnumerator RestoreSpikeBall()
    {
        yield return new WaitForSeconds(1.5f);
        canMove = false;
        i = 0;
        animator.SetBool("LeftRotation", false);
        animator.SetBool("RightRotation", false);
        transform.parent.position = parentStartPosition;
        transform.position = startPosition;
        transform.rotation = startRotation;
        enemyDamage.GetComponent<DamageObject>().damage = 10;
        GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
}

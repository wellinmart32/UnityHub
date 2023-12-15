using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerLevel03 : MonoBehaviour
{
    public bool isArrive;
    public float separation;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float speed = 1f;
    private GameObject player;
    private FollowingArea followingArea;
    private void Start()
    {
        player = GameObject.Find("Player");
        followingArea = GameObject.Find("FollowingArea").GetComponent<FollowingArea>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (followingArea.canFollow)
        {
            FollowPlayer();
        }

    }

    private void FollowPlayer()
    {
        StartCoroutine(CheckMoving());
        Vector3 separationVector = new Vector3(separation, 0, 0);

        if (transform.position.x <= player.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position - separationVector, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position + separationVector, speed * Time.deltaTime);
        }
    }

    private IEnumerator CheckMoving()
    {
        yield return new WaitForSeconds(0);

        if (transform.position.x - separation  <= player.transform.position.x + 0.005f
            && transform.position.x + separation >= player.transform.position.x - 0.005f
            || !followingArea.canFollow)
        {
            animator.SetBool("Idle", true);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Idle", false);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Idle", false);
        }
    }
}

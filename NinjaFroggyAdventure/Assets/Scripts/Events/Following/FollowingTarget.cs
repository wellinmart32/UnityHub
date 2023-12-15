using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingTarget : MonoBehaviour
{
    public bool canFollow = true;
    public float separation;
    public GameObject target;
    public float speed = 1.4f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private FollowingArea followingArea;

    private void Awake()
    {
        target = GameObject.Find("Player");
        followingArea = GetTopParent(3).GetComponent<FollowingArea>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (followingArea.canFollow && canFollow)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {

        StartCoroutine(CheckMoving());
        
        Vector3 separationVector = new Vector3(separation, 0, 0);

        if (transform.position.x <= target.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position - separationVector, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position + separationVector, speed * Time.deltaTime);
        }
    }

    private IEnumerator CheckMoving()
    {
        yield return new WaitForSeconds(0);

        if (transform.position.x - separation <= target.transform.position.x + 0.005f
            && transform.position.x + separation >= target.transform.position.x - 0.005f
            || !followingArea.canFollow)
        {
            if (GetComponentInParent<RespawnEnemy>().type != RespawnEnemy.Enemy.Gost)
            {
                animator.SetBool("Idle", true);
            }
        }
        else if (transform.position.x > target.transform.position.x)
        {
            spriteRenderer.flipX = false;

            if (GetComponentInParent<RespawnEnemy>().type != RespawnEnemy.Enemy.Gost)
            {
                animator.SetBool("Idle", false);
            }
        }
        else if (transform.position.x < target.transform.position.x)
        {
            spriteRenderer.flipX = true;

            if (GetComponentInParent<RespawnEnemy>().type != RespawnEnemy.Enemy.Gost)
            {
                animator.SetBool("Idle", false);
            }
        }
    }

    // revisar si se requiere ingresar en clase UI
    private Transform GetTopParent(int parentPosition)
    {
        int counter = 0;
        Transform topParent = gameObject.transform;

        while (counter < parentPosition - 1)
        {
            if (topParent.parent != null)
            {
                topParent = topParent.parent;
            }
            else
            {
                return topParent;
            }

            counter++;
        }
        
        return topParent;
    }
}

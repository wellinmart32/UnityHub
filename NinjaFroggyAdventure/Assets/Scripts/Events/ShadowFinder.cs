using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFinder : MonoBehaviour
{
    public float xSeparation = 0.5f;
    public float ySeparation = 0.5f;
    public float speed = 0.2f;
    public GameObject target;
    public float startWaitTime = 5f;
    public float flipWaitTime = 5f;
    private float waiTtime;
    private bool randomXFlip;
    public bool canFollow = true;
    private FollowingArea followingArea;
    //private Animator animator;

    private void Awake()
    {
        target = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        followingArea = GetComponentInParent<FollowingArea>();
        waiTtime = startWaitTime;
    }

    private void Update()
    {
        if (followingArea.canFollow && canFollow)
        {
            FollowTarget();
            XFlipCount();
        }
    }

    private void FollowTarget()
    {
        if (randomXFlip)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position + new Vector3(xSeparation, ySeparation), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position + new Vector3(-xSeparation, ySeparation), speed * Time.deltaTime);
        }
    }

    private void XFlipCount()
    {
        waiTtime -= Time.deltaTime;

        if (waiTtime <= 0)
        {
            SwitchXFlip();
            waiTtime = startWaitTime;
        }
    }

    private void SwitchXFlip()
    {
        int num = Random.Range(0, 2);

        if (num == 0)
        {
            randomXFlip = true;
        }
        else
        {
            randomXFlip = false;
        }
        Debug.Log(randomXFlip);
    }

    //private IEnumerator CheckMoving()
    //{
    //    yield return new WaitForSeconds(waitTime);

    //    if (transform.position.x - separation <= target.transform.position.x + 0.005f
    //        && transform.position.x + separation >= target.transform.position.x - 0.005f
    //        || !followingArea.canFollow)
    //    {
    //        if (GetComponentInParent<RespawnEnemy>().type != RespawnEnemy.Enemy.Gost)
    //        {
    //            animator.SetBool("Idle", true);
    //        }
    //    }
    //    else if (transform.position.x > target.transform.position.x)
    //    {
    //        spriteRenderer.flipX = false;

    //        if (GetComponentInParent<RespawnEnemy>().type != RespawnEnemy.Enemy.Gost)
    //        {
    //            animator.SetBool("Idle", false);
    //        }
    //    }
    //    else if (transform.position.x < target.transform.position.x)
    //    {
    //        spriteRenderer.flipX = true;

    //        if (GetComponentInParent<RespawnEnemy>().type != RespawnEnemy.Enemy.Gost)
    //        {
    //            animator.SetBool("Idle", false);
    //        }
    //    }
    //}
}

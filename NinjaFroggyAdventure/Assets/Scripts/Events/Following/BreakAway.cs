using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAway : MonoBehaviour
{
    public Vector2 reboundSpeed;
    private bool canSeparate;
    private FollowingTarget followingTarget;

    private void Start()
    {
        followingTarget = transform.GetComponent<FollowingTarget>();
    }

    private void Update()
    {
        if (followingTarget.target.GetComponent<Animator>() != null)
        {
            if (followingTarget.target.GetComponent<Animator>().GetBool("Hit"))
            {
                canSeparate = true;
            }

            if (canSeparate)
            {
                if (Vector2.Distance(transform.position, followingTarget.target.transform.position) <= 0.5f)
                { 
                    followingTarget.canFollow = false;
                    BreakAwayFromTarjet();
                    Invoke("RestoreFollowingTarjet", 1);
                }
            }
        }
    }

    private void BreakAwayFromTarjet()
    {
        if (followingTarget.target.transform.position.x <= transform.position.x)
        {
            transform.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(reboundSpeed.x, reboundSpeed.y);
        }
        else
        {
            transform.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(-reboundSpeed.x, reboundSpeed.y);
        }
    }

    private void RestoreFollowingTarjet()
    {
        followingTarget.canFollow = true;
        canSeparate = false;
    }

}

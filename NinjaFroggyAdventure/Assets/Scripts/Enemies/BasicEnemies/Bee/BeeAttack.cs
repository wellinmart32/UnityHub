using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : MonoBehaviour
{
    public float distanceRaycast = 1.5f;
    public float cooldownAttack = 1.5f;
    public Animator animator;
    public GameObject beeBullet;
    private float currentCooldownAttack = 0;
    
    void Awake()
    {
        currentCooldownAttack = 0;
    }

    void Update()
    {
        currentCooldownAttack -= Time.deltaTime;
        //Debug.DrawRay(transform.position, Vector2.down, Color.red, distanceRaycast);
    }

    void FixedUpdate()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, distanceRaycast);

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.CompareTag("Player"))
            {
                if (currentCooldownAttack < 0)
                {
                    Invoke("LaunchBullet", 0.1f);
                    //animator.SetBool("Attack", true);
                    animator.Play("Attack");
                    //Invoke("DisableAttack", 0.5f);
                    currentCooldownAttack = cooldownAttack;
                }
            }
        }
    }

    private void DisableAttack()
    {
        animator.SetBool("Attack", false);
    }

    private void LaunchBullet()
    {
        GameObject newBullet = Instantiate(beeBullet, transform.position, transform.rotation);
    }
}

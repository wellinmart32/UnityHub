using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostGuardianMove : MonoBehaviour
{
    public float horizontalRunSpeed = 2;
    public bool canMove = true;
    public float runSpeed = 2;
    public float swimmingForce = 500;
    public float waterFriction = 0.65f;
    public bool enableAttack;
    public float attackWaitTime;
    public float attackingTime;
    public GameObject damageArea;
    private float horizontalMove = 0f;
    private Joystick joystick;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        joystick = GameObject.Find("StandarInterface").GetComponent<Initialization>().mobileControls.GetComponentInChildren<FixedJoystick>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
        {
            if (horizontalMove > 0)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Walk", true);
            }
            else if (horizontalMove < 0)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }

            if (CheckWater.inWater)
            {
                rb2D.drag = 10;
            }
            else
            {
                rb2D.drag = 1;
            }

            if (!spriteRenderer.flipX)
            {
                damageArea.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                damageArea.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            horizontalMove = joystick.Horizontal * horizontalRunSpeed;

            if (enableAttack || Input.GetKey("z"))
            {
                StartCoroutine(Attack());
            }

            if (joystick.Horizontal == 0)
            {
                horizontalMove = 0;
            }

            Move();
        }
    }

    private void Move()
    {
        if (horizontalMove > 0)
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
        }
        else if (horizontalMove < 0)
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }

    public void EnableAttack()
    {
        enableAttack = true;
    }

    private IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(attackingTime);
        damageArea.SetActive(true);
        yield return new WaitForSeconds(attackWaitTime);
        enableAttack = false;
        damageArea.SetActive(false);
        animator.SetBool("Attack", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveJoystick : MonoBehaviour
{
    public float horizontalRunSpeed = 2;
    public bool canMove = true;
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float swimmingForce = 500;
    public float waterFriction = 0.65f;
    public bool betterJump = false;
    public AudioSource swimAS;
    private float horizontalMove = 0f;
    public Joystick joystick;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
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
                spriteRenderer.flipX = false;
                animator.SetBool("Skip", true);
            }
            else if (horizontalMove < 0)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Skip", true);
            }
            else
            {
                animator.SetBool("Skip", false);
            }

            if (CheckGround.isGrounded == false)
            {
                animator.SetBool("Jump", true);
                animator.SetBool("Skip", false);
            }

            if (CheckGround.isGrounded == true)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", false);
            }

            if (rb2D.velocity.y < 0)
            {
                animator.SetBool("Fall", true);
            }
            else if (rb2D.velocity.y > 0)
            {
                animator.SetBool("Fall", false);
            }

            if (CheckWater.inWater)
            {
                rb2D.drag = 10;
            }
            else
            {
                rb2D.drag = 1;
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
            animator.SetBool("Skip", false);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            horizontalMove = joystick.Horizontal * horizontalRunSpeed;

            if (joystick.Horizontal == 0)
            {
                horizontalMove = 0;
            }

            Move();
        }

        if (Input.GetKey("space"))
        {
            Jump();
        }
    }

    private void Move()
    {
        if (horizontalMove > 0 || Input.GetKey("d"))
        {
            if (!CheckWater.inWater)
            {
                rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            }
            else
            {
                rb2D.velocity = new Vector2(runSpeed * waterFriction, rb2D.velocity.y);
            }

            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else if (horizontalMove < 0 || Input.GetKey("a"))
        {
            if (!CheckWater.inWater)
            {
                rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            }
            else
            {
                rb2D.velocity = new Vector2(-runSpeed * waterFriction, rb2D.velocity.y);
            }

            spriteRenderer.flipX = true;
            animator.SetBool("Skip", true);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }

    public void Jump()
    {
        if (canMove)
        {
            if (CheckGround.isGrounded)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            }

            if (CheckWater.inWater)
            {
                swimAS.Play();
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                rb2D.AddForce(new Vector2(0, swimmingForce), ForceMode2D.Force);
            }
        }
    }


    public IEnumerator EnableMove(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}

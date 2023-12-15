using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveJoystick : MonoBehaviour
{
    public float horizontalRunSpeed = 2;
    public bool canMove = true;
    public float runSpeed = 2;
    public float jumpSpeed = 3, doubleJumpSpeed = 2.5f;
    public float swimmingForce = 500;
    public float waterFriction = 0.65f;
    public bool betterJump = false, canDoubleJump;
    public GameObject dustParticleLeft;
    public GameObject dustParticleRight;
    private bool canTripleJump;
    private string tripleJumpName;
    private bool isTripleJumpEnabled;
    private string wallSlidingName;
    private bool canWallSliding;
    public float wallSlidingSpeed = 0.75f;
    public GameObject dashParticle;
    public bool isDashing;
    public AudioSource swimAS;
    private float horizontalMove = 0f;
    private Joystick joystick;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool wallSliding;
    private PlayerSkillDataManager playerSkillDataManager;

    private void Awake()
    {
        playerSkillDataManager = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerSkillDataManager;
        joystick = GameObject.Find("StandarInterface").GetComponent<Initialization>().mobileControls.GetComponentInChildren<FixedJoystick>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        dustParticleLeft.SetActive(false);
        dustParticleRight.SetActive(false);
    }

    private void Start()
    {
        isTripleJumpEnabled = playerSkillDataManager.GetSkillAvailability(tripleJumpName);
    }

    private void Update()
    {
        if (canMove)
        {
            if (!isDashing)
            {
                animator.SetBool("Dash", false);
            }
            else
            {
                animator.SetBool("Dash", true);
            }

            if (horizontalMove > 0 && !isDashing)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Run", true);
            }
            else if (horizontalMove < 0 && !isDashing)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }

            if (CheckGround.isGrounded == false)
            {
                animator.SetBool("Jump", true);
                animator.SetBool("Run", false);
            }

            if (CheckGround.isGrounded == true)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("DoubleJump", false);
                animator.SetBool("TripleJump", false);
                animator.SetBool("Falling", false);
            }

            if (rb2D.velocity.y < 0)
            {
                animator.SetBool("Falling", true);
            }
            else if (rb2D.velocity.y > 0)
            {
                animator.SetBool("Falling", false);
            }

            if ((LeftTouchingWall.isTouchingWall || RightTouchingWall.isTouchingWall) && CheckGround.isGrounded == false)
            {
                wallSliding = true;
            }
            else
            {
                wallSliding = false;
            }

            if (wallSliding)
            {
                animator.Play("WallJump");
                rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
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
            animator.SetBool("DoubleJump", false);
            animator.SetBool("TripleJump", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Run", false);
            animator.SetBool("Dash", false);
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
        if (!isDashing)
        {
            if (horizontalMove > 0 || Input.GetKey("d") && RightTouchingWall.isTouchingWall == false)
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
            else if (horizontalMove < 0 || Input.GetKey("a") && LeftTouchingWall.isTouchingWall == false)
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
                animator.SetBool("Run", true);
            }
            else
            {
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            }
        }
    }

    public void Jump()
    {
        if (canMove)
        {
            if (!isDashing)
            {
                if (CheckGround.isGrounded)
                {
                    canDoubleJump = true;
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        canTripleJump = true;
                        animator.SetBool("DoubleJump", true);
                        rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                        canDoubleJump = false;
                    }
                    else if (isTripleJumpEnabled)
                    {
                        if (canTripleJump)
                        {
                            animator.SetBool("TripleJump", true);
                            rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                            canTripleJump = false;
                        }
                    }
                }

                if (CheckWater.inWater)
                {
                    swimAS.Play();
                    rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                    rb2D.AddForce(new Vector2(0, swimmingForce), ForceMode2D.Force);
                }
            }
        }
    }

    public IEnumerator EnableMove(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}

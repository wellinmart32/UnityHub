using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool canMove = true;
    public float runSpeed = 2;
    public float jumpSpeed = 3, doubleJumpSpeed = 2.5f;
    public float swimmingForce = 500;
    public float waterFriction = 0.65f;
    public bool betterJump = false, canDoubleJump;
    public float dashCooldown;
    public float dashForce = 30;
    public float wallSlidingSpeed = 0.75f;
    public GameObject dashParticle;
    public AudioSource swimAS;
    public GameObject dustParticleLeft;
    public GameObject dustParticleRight;
    private Rigidbody2D rb2D;
    private float fallMultiplayer = 0.5f;
    private float lowJumpMultiplayer = 1f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isTouchingWall = false;
    private bool wallSliding;
    private bool isTouchingLeft;
    private bool isTouchingRight;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        dustParticleLeft.SetActive(false);
        dustParticleRight.SetActive(false);
    }

    private void Update()
    {
        dashCooldown -= Time.deltaTime;

        if (canMove)
        {
            if (Input.GetKey("space") && wallSliding == false)
            {
                if (!CheckWater.inWater)
                {
                    if (CheckGround.isGrounded)
                    {
                        canDoubleJump = true;
                        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                    }
                    else
                    {
                        if (Input.GetKeyDown("space"))
                        {
                            if (canDoubleJump)
                            {
                                animator.SetBool("DoubleJump", true);
                                rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                                canDoubleJump = false;
                            }
                        }
                    }
                }
            }

            if (CheckGround.isGrounded == false)
            {
                animator.SetBool("Jump", true);
                animator.SetBool("Run", false);
                dustParticleLeft.SetActive(false);
                dustParticleRight.SetActive(false);
            }

            if (CheckGround.isGrounded == true)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("DoubleJump", false);
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

            if (isTouchingWall == true && CheckGround.isGrounded == false)
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

                if (Input.GetKey("space"))
                {
                    swimAS.Play();
                    rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                    rb2D.AddForce(new Vector2(0, swimmingForce), ForceMode2D.Force);
                }
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
            animator.SetBool("Falling", false);
            animator.SetBool("Run", false);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (Input.GetKey("d") || Input.GetKey("right") && isTouchingRight == false)
            {
                if (!CheckWater.inWater)
                {
                    rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);

                }
                else
                {
                    rb2D.velocity = new Vector2(runSpeed*waterFriction, rb2D.velocity.y);
                }

                spriteRenderer.flipX = false;
                animator.SetBool("Run", true);

                if (CheckGround.isGrounded == true && !CheckWater.inWater)
                {
                    dustParticleLeft.SetActive(true);
                    dustParticleRight.SetActive(false);
                }

                if (Input.GetKey("e") && dashCooldown <= 0)
                {
                    Dash();
                }
            }
            else if (Input.GetKey("e") && dashCooldown <= 0)
            {
                Dash();
            }
            else if (Input.GetKey("a") || Input.GetKey("left") && isTouchingLeft == false)
            {
                if (!CheckWater.inWater)
                {
                    rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
                }
                else
                {
                    rb2D.velocity = new Vector2(-runSpeed*waterFriction, rb2D.velocity.y);
                }
                spriteRenderer.flipX = true;
                animator.SetBool("Run", true);

                if (CheckGround.isGrounded == true && !CheckWater.inWater)
                {
                    dustParticleLeft.SetActive(false);
                    dustParticleRight.SetActive(true);
                }

                if (Input.GetKey("e") && dashCooldown <= 0)
                {
                    Dash();
                }

            }
            else
            {
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
                animator.SetBool("Run", false);
                dustParticleLeft.SetActive(false);
                dustParticleRight.SetActive(false);
            }

            if (CheckGround.isGrounded == true)
            {
                animator.SetBool("Jump", false);
            }

            if (betterJump)
            {
                if (rb2D.velocity.y < 0)
                {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplayer * Time.deltaTime;
                }

                if (rb2D.velocity.y > 0 && !Input.GetKey("space"))
                {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplayer * Time.deltaTime;
                }
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Run", false);
        }
    }

    public void Dash()
    {
        GameObject dashObject = Instantiate(dashParticle,transform.position, transform.rotation);

        if (spriteRenderer.flipX == true)
        {
            rb2D.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
        }
        else
        {
            rb2D.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
        }

        dashCooldown = 2;
        Destroy(dashObject, 1);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RightWall"))
        {
            isTouchingWall = true;
            isTouchingRight = true;
        }

        if (collision.gameObject.CompareTag("LeftWall"))
        {
            isTouchingWall = true;
            isTouchingLeft = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingWall = false;
        isTouchingRight = false;
        isTouchingLeft = false;
    }

    public IEnumerator EnableMove(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}

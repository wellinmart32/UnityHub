using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirySkill : MonoBehaviour
{
    public float runSpeed = 0.5f;
    public float deadTime = 4f;
    public Collider2D firyColl2D;
    private bool canMove;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Dead());
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Run", true);
            canMove = true;
        }
    }

    private void Move()
    {
        if (spriteRenderer.flipX)
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
        }
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(deadTime);
        Destroy(gameObject);
    }
}

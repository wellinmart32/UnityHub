using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D bC2D;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        bC2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            bC2D.offset = new Vector2(-bC2D.offset.x, bC2D.offset.y);
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}

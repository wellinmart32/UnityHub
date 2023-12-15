using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash")]
    public float dashForce = 3f;
    public AudioSource dashSound;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float timeCanDash = 1f;
    private GameObject player;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private bool canDash;
    private float baseGravity;
    private PlayerMoveJoystick playerMoveJoystick;
    private bool dashInCooldown;
    private bool isDashing;
    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        playerMoveJoystick = player.GetComponent<PlayerMoveJoystick>();
        rb2D = player.GetComponent<Rigidbody2D>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        baseGravity = rb2D.gravityScale;
    }

    private void FixedUpdate()
    {
        if (playerMoveJoystick.canMove)
        {
            if (canDash && !isDashing && !dashInCooldown)
            {
                StartCoroutine(PlayerDash());
            }
            else if (Input.GetKey("e") && !isDashing && !dashInCooldown)
            {
                StartCoroutine(PlayerDash());
            }
        }
    }

    public IEnumerator PlayerDash()
    {
        dashSound.Play();
        playerMoveJoystick.isDashing = true;
        isDashing = true;
        dashInCooldown = true;
        rb2D.gravityScale = 0f;

        if (spriteRenderer.flipX == true)
        {
            rb2D.velocity = new Vector2((-dashForce + rb2D.velocity.x), 0f);
        }
        else
        {
            rb2D.velocity = new Vector2((dashForce + rb2D.velocity.x), 0f);
        }

        yield return new WaitForSeconds(dashingTime);
        playerMoveJoystick.isDashing = false;
        isDashing = false;
        rb2D.gravityScale = baseGravity;
        yield return new WaitForSeconds(timeCanDash);
        canDash = false;
        dashInCooldown = false;
    }

    public void EnableDash()
    {
        canDash = true;
    }
}

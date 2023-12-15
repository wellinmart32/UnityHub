using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{
    public Vector3 startPosition;
    public bool canMove = true;
    public float feintTime = 2f;
    [SerializeField] private Transform player;
    [SerializeField] private float distance;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player.transform;
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("Distance", distance);

        if (player.GetComponent<PlayerRespawn>().GetCurrentHearts() <= 0)
        {
            animator.SetTrigger("Return");
        }
    }

    public void FlipBat(Vector3 target)
    {
        if (transform.position.x < target.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public IEnumerator GotFeint()
    {
        canMove = false;
        yield return new WaitForSeconds(feintTime);
        canMove = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSnail : MonoBehaviour
{
    public int distanceRaycast = 3;
    public Animator animator;
    public Rigidbody2D rb2D;
    public GameObject enemy;
    public BasicAI basicAI;
    public float dieTime = 5;

    private void FixedUpdate()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, distanceRaycast);
        Debug.DrawRay(transform.position, Vector2.down, Color.red, distanceRaycast);

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.CompareTag("Player"))
            {
                basicAI.canMove = false;
                animator.Play("ShellIdle");
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                //rb2D.AddForce(-transform.up);
                StartCoroutine(EnemyDie());
            }
        }
    }

    private IEnumerator EnemyDie()
    {
        yield return new WaitForSeconds(dieTime);
        enemy.SetActive(false);
    }
}

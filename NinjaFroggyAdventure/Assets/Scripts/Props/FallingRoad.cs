using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRoad : MonoBehaviour
{
    public GameObject fallingSprite;
    public bool canFall = true;
    public float waitTime = 2;
    private float restoreTime = 2;
    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private Vector2 startPosition;
    
    private void Awake()
    {
        fallingSprite = transform.GetChild(0).gameObject;
        rb2D = fallingSprite.GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        startPosition = fallingSprite.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (canFall)
            {
                canFall = false;
                StartCoroutine(DropRoad());
            }
        }
    }

    public IEnumerator DropRoad()
    {
        yield return new WaitForSeconds(waitTime);
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.AddForce(-transform.up);
        bc2D.enabled = false;
        StartCoroutine(EnableRoad());
    }

    private IEnumerator EnableRoad()
    {
        yield return new WaitForSeconds(restoreTime);
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        bc2D.enabled = true;
        fallingSprite.transform.position = startPosition;
        canFall = true;
    }
}

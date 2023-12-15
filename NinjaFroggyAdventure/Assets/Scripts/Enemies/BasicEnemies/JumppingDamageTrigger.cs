using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumppingDamageTrigger : MonoBehaviour
{
    //public Collider2D collider2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject destroyParticle;
    public float jumpForce = 2.5f;
    public int lifes = 2;
    public GameObject enemy;
    public AudioSource clip;
    public int damage;
    private AISounds aISounds;

    private void Awake()
    {
        aISounds = GetComponentInChildren<AISounds>();
        clip = GameObject.Find("EnemyHit").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            clip.Play();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * jumpForce);
            damage = 1;
            LoseLifeAndHit();
        }
    }

    public  void LoseLifeAndHit()
    {
        lifes = lifes - damage;
        animator.Play("Hit");
        CheckLifes();
    }

    public void CheckLifes()
    {
        if (lifes <= 0)
        {
            if (aISounds)
            {
                aISounds.canListen = false;
            }

            destroyParticle.SetActive(true);
            spriteRenderer.enabled = false;
            Invoke("EnemyDie", 0.2f);
        }
    }

    public void EnemyDie()
    {
        var jdt = GetComponentInChildren<DamageObjectTrigger>();

        if (enemy.GetComponent<RespawnEnemy>())
        {
            enemy.GetComponent<RespawnEnemy>().canRestore = false;
        }

        enemy.SetActive(false);

        if (jdt)
        {
            jdt.DisableDamage();
        }
    }
}

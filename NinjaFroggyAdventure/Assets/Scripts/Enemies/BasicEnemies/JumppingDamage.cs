using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumppingDamage : MonoBehaviour
{
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

    private void OnCollisionEnter2D(Collision2D collision)
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
        //animator.Play("Hit");
        if (animator != null) animator.SetBool("Hit", true);
        Invoke("DisableHit", 0.25f);
        CheckLifes();
    }

    private void DisableHit()
    {
        if (animator != null)  animator.SetBool("Hit", false);
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
        var jd = GetComponentInChildren<DamageObject>();

        if (enemy.GetComponent<RespawnEnemy>())
        {
            enemy.GetComponent<RespawnEnemy>().canRestore = false;
        }

        enemy.SetActive(false);

        if (jd)
        {
            jd.DisableDamage();
        }
    }
}

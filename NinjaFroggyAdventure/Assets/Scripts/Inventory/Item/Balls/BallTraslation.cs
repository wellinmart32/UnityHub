using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTraslation : MonoBehaviour
{
    public float speed = 3.5f;
    public float lifeTime = 2;
    public int damage = 1;
    public float dactivateTime = 0.05f;
    public AudioSource clip;

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponentInParent<JumppingDamage>())
            {
                collision.gameObject.GetComponentInParent<JumppingDamage>().damage = damage;
                collision.gameObject.GetComponentInParent<JumppingDamage>().LoseLifeAndHit();
            }

            if (collision.gameObject.GetComponentInParent<JumppingDamageTrigger>())
            {
                collision.gameObject.GetComponentInParent<JumppingDamageTrigger>().damage = damage;
                collision.gameObject.GetComponentInParent<JumppingDamageTrigger>().LoseLifeAndHit();
            }

            clip.Play();
            Destroy(gameObject, dactivateTime);
            return;
        }

        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (collision.gameObject.GetComponent<Bomb>())
            {
                collision.gameObject.GetComponent<Bomb>().Explosion();
            }

            clip.Play();
            Destroy(gameObject, dactivateTime);
            return;
        }
    }
}

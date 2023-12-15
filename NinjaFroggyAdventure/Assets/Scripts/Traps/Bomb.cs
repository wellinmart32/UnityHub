using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float radio = 5;
    public float explosionForce = 500;
    public bool canExplote = true;
    public float disableTime = 1f;
    public GameObject explotonEffect;
    private AISounds AISounds;

    private void Awake()
    {
        AISounds = GetComponentInChildren<AISounds>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        if (canExplote)
        {
            AISounds.canListen = true;
            canExplote = false;
            explotonEffect = Instantiate(explotonEffect, transform.position, Quaternion.identity);
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radio);


            foreach (Collider2D ob in objects)
            {
                Rigidbody2D rb2D = ob.GetComponent<Rigidbody2D>();

                if (rb2D != null)
                {
                    Vector2 direction = ob.transform.position - transform.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = explosionForce / distance;
                    rb2D.AddForce(direction * finalForce);

                    if (ob.CompareTag("Player"))
                    {
                        GetComponentInChildren<DamageObject>().PlayerDamaged(ob.GetComponent<PlayerRespawn>());
                    }
                }

                if (ob.CompareTag("Enemy"))
                {
                    if (ob.GetComponentInParent<JumppingDamage>())
                    {
                        ob.GetComponentInParent<JumppingDamage>().damage = GetComponentInChildren<DamageObject>().damage;
                        ob.GetComponentInParent<JumppingDamage>().LoseLifeAndHit();
                    }

                    if (ob.GetComponentInParent<JumppingDamageTrigger>())
                    {
                        ob.GetComponentInParent<JumppingDamageTrigger>().damage = GetComponentInChildren<DamageObject>().damage;
                        ob.GetComponentInParent<JumppingDamageTrigger>().LoseLifeAndHit();
                    }
                }

                if (ob.CompareTag("Bomb"))
                {
                    if (ob.GetComponent<Bomb>() != null)
                    {
                        ob.GetComponent<Bomb>().Explosion();
                    }
                }
            }

            StartCoroutine(DisableBomb());
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, radio);
    }

    public IEnumerator DisableBomb()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(1);
        AISounds.canListen = false;
        Destroy(explotonEffect, disableTime);
        yield return new WaitForSecondsRealtime(disableTime);
        Destroy(gameObject, 0.1f);
    }
}

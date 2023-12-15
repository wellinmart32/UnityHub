using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword2D : MonoBehaviour
{
    public Animator animator;
    public GameObject sword2D;
    public int damage;
    public Item item;
    public GameObject sword;
    public AudioSource[] clips;
    private PolygonCollider2D coll2D;
    private SpriteRenderer playerSP;

    void Awake()
    {
        GameObject player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        playerSP = player.GetComponent<SpriteRenderer>();
        coll2D = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.Q)/* || Input.GetMouseButtonDown(0)*/)
            {
                Attack();
            }

            if (playerSP.flipX == true)
            {
                sword2D.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                sword2D.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void Attack()
    {
        clips[1].Play();
        animator.Play("Attack");
        coll2D.enabled = true;
        Invoke("DisableAttack", 0.5f);
    }

    private void DisableAttack()
    {
        coll2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            clips[0].Play();

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

            Slot slot = GameObject.Find("StandarInterface").GetComponent<Initialization>().inventory.GetComponent<Inventory>().slots[item.id].GetComponent<Slot>();
            item.DecreaseDurability(slot);
        }

        if (collision.gameObject.CompareTag("Bomb"))
        {
            clips[0].Play();

            if (collision.gameObject.GetComponent<Bomb>())
            {
                collision.gameObject.GetComponent<Bomb>().Explosion();
            }
        }

        coll2D.enabled = false;
    }
}

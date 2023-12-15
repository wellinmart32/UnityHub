using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjectTrigger : MonoBehaviour
{
    public int damage;
    public bool isSuperAttack;
    public AudioSource clip;

    private void Awake()
    {
        clip = GameObject.Find("PlayerHit").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            clip.Play();
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged(damage, isSuperAttack);
        }
    }

    public void DisableDamage()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void EnableDamage()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}


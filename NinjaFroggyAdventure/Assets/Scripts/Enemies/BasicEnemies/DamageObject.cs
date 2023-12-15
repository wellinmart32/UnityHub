using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int damage;
    public bool isSuperAttack;
    public AudioSource clip;

    private void Awake()
    {
        clip = GameObject.Find("PlayerHit").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (clip)
            {
                clip.Play();
            }
            
            PlayerRespawn playerRespawn = collision.transform.GetComponent<PlayerRespawn>();
            FrostGuardianLife frostGuardianLife = collision.transform.GetComponent<FrostGuardianLife>();

            if (playerRespawn)
            {
                PlayerDamaged(playerRespawn);
            }
            else if (frostGuardianLife)
            {
                PlayerDamaged(frostGuardianLife);
            }
        }
    }

    public void PlayerDamaged(PlayerRespawn playerRespawn)
    {
        if (damage > 0)
        {
            playerRespawn.PlayerDamaged(damage, isSuperAttack);
        }
    }

    public void PlayerDamaged(FrostGuardianLife frostGuardianLife)
    {
        if (damage > 0)
        {
            frostGuardianLife.Hit();
        }
    }

    public void DisableDamage()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableDamage()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemies : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var jD = collision.gameObject.GetComponentInChildren<JumppingDamage>();
            var jDT = collision.gameObject.GetComponentInChildren<JumppingDamageTrigger>();

            if (jD)
            {
                int previousDamage = jD.damage;
                jD.damage = damage;
                jD.LoseLifeAndHit();
                jD.damage = previousDamage;
            }
            else if (jDT)
            {
                int previousDamage = jDT.damage;
                jDT.damage = damage;
                jDT.LoseLifeAndHit();
                jDT.damage = previousDamage;
            }
        }
    }
}

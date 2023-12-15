using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged(damage, false);
        }
    }
}

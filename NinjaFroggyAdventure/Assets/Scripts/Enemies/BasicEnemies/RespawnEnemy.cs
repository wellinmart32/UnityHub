using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    public Vector2 startPosition;
    public GameObject destroyParticle;
    public SpriteRenderer spriteRenderer;
    public GameObject enemy;
    public bool canRestore;
    public enum Enemy { Mushroom, FollowerMushroom, Bat, Plant, Bee, BlueBird, FallingSnail, Gost }
    public Enemy type;
    private DamageObject damObj;
    private DamageObjectTrigger damObjTri;

    void Awake()
    {
        startPosition = gameObject.transform.position;
        damObj = GetComponentInChildren<DamageObject>();
        damObjTri = GetComponentInChildren<DamageObjectTrigger>();
    }

    public void RestoreEnemy()
    {
        if (type == Enemy.FallingSnail)
        {
            Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            rb2D.velocity = new Vector2(0, 0);
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponentInChildren<BasicAI>().canMove = true;
        }

        if (type != Enemy.Bat)
        {
            enemy.transform.parent.position = startPosition;
        }

        if (damObj)
        {
            damObj.EnableDamage();
        }

        if (damObjTri)
        {
            damObjTri.EnableDamage();
        }

        enemy.transform.position = startPosition;
        gameObject.SetActive(true);
        spriteRenderer.enabled = true;
        destroyParticle.SetActive(false);
        RestoreEnemyLifes();
    }

    private void RestoreEnemyLifes()
    {
        switch (type)
        {
            case Enemy.Mushroom:
                enemy.GetComponent<JumppingDamage>().lifes = 2;
                break;
            case Enemy.FollowerMushroom:
                enemy.GetComponent<JumppingDamage>().lifes = 3;
                break;
            case Enemy.Bat:
                enemy.GetComponent<JumppingDamageTrigger>().lifes = 2;
                break;
            case Enemy.Plant:
                enemy.GetComponent<JumppingDamage>().lifes = 1;
                break;
            case Enemy.Bee:
                enemy.GetComponent<JumppingDamage>().lifes = 2;
                break;
            case Enemy.BlueBird:
                enemy.GetComponent<JumppingDamage>().lifes = 3;
                break;
            case Enemy.FallingSnail:
                break;
            case Enemy.Gost:
                break;
            default:
                break;
        }
    }
}

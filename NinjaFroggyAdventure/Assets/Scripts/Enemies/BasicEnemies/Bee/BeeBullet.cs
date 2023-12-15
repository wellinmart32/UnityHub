using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    public float speed = 4;
    public float lifeTime = 2;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}

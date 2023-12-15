using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetElement : MonoBehaviour
{
    public float magnetDistance;
    public GameObject element;
    private GameObject player;
    private float velocity = 5f;

    private void Awake()
    {
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
    }

    private void Update()
    {
        if (Vector2.Distance(element.transform.position, player.transform.position) <= magnetDistance)
        {
            element.transform.position = Vector2.MoveTowards(element.transform.position, player.transform.position, velocity * Time.deltaTime);
        }
    }
}

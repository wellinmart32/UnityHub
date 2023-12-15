using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundVerificator : MonoBehaviour
{
    public static bool isInsideGround;
    public static bool wasMoved;

    public Vector2 startPosition;
    public Vector2 currentPosition;
    public bool isInsideGround2;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        currentPosition = transform.position;

        if (startPosition != currentPosition)
        {
            Debug.Log("Verificador fue movido de " + startPosition + " a " + currentPosition + " y el isInsideGround es " + isInsideGround);
            wasMoved = true;
            currentPosition = startPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isInsideGround = true;
            isInsideGround2 = true;
            //alreadyPositioned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isInsideGround = false;
            isInsideGround2 = false;
        }
    }
}

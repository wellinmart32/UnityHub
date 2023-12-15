using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrampolineJumpForce : MonoBehaviour
{
    public float nextJumpForce;
    public float currentJumpForce;
    public Trampoline trampoline;
    [SerializeField] private int count = 3;
    private int startCount;

    private void Awake()
    {
        currentJumpForce = trampoline.jumpForce;
        startCount = count;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && trampoline.transform.parent.gameObject.activeInHierarchy)
        {
            if (count > 0)
            {
                count--;
            }
            else if (count == 0)
            {
                trampoline.jumpForce = nextJumpForce;
                count--;
            }
            else
            {
                trampoline.jumpForce = currentJumpForce;
                count = startCount;
            }
        }
    }
}

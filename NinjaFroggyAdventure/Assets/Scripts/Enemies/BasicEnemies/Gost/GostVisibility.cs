using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostVisibility : MonoBehaviour
{
    public Animator animator;
    public GameObject gost;
    public float disappearWaitTime;
    public float appearWaitTime;
    public GostVisibilityWatcher gostVisibilityWatcher;

    public IEnumerator DisappearGost()
    {
        animator.SetBool("Appear", false);
        animator.SetBool("Idle", false);
        yield return new WaitForSeconds(disappearWaitTime);
        gost.SetActive(false);
    }

    public void AppearGost()
    {
        animator.SetBool("Appear", true);
        animator.SetBool("Idle", true);
        gost.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(DisappearGost());
            gostVisibilityWatcher.canAppear = true;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankObserver : MonoBehaviour
{
    public GameObject element;
    public bool nextState = true;
    public int[] correctActivationOrder;
    public int[] activationOrder;
    public AudioSource[] clips;
    private bool canPlaySuccessfully = true;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<ActivateCrank>().position = i + 1;
        }

        activationOrder = new int[transform.childCount];
    }

    private void Update()
    {
        SwitchState();
        RestoreWrongCranks();
    }

    private bool CheckActivationOrder()
    {
        bool canActivate = true;

        for (int i = 0; i < activationOrder.Length; i++)
        {
            if (activationOrder[i] != 0)
            {
                if (activationOrder[i] != correctActivationOrder[i])
                {
                    canActivate = false;
                }
            }
            else
            {
                canActivate = false;
            }
        }

        return canActivate;
    }

    private void SwitchState()
    {
        if (CheckActivationOrder())
        {
            if (clips[0])
            {
                if (!clips[0].isPlaying && canPlaySuccessfully)
                {
                    clips[0].Play();
                    canPlaySuccessfully = false;
                }
            }
            
            element.SetActive(nextState);
        }
    }

    private bool CanRestoreList()
    {
        bool canRestore = true;

        for (int i = 0; i < activationOrder.Length; i++)
        {
            if (activationOrder[i] == 0)
            {
                canRestore = false;
            }
        }

        return canRestore;
    }

    private bool VerifyActivationOrder()
    {
        bool isWrong = false;

        if (CanRestoreList())
        {
            for (int i = 0; i < activationOrder.Length; i++)
            {
                if (activationOrder[i] != correctActivationOrder[i])
                {
                    isWrong = true;
                }
            }
        }

        return isWrong;
    }

    private void RestoreWrongCranks()
    {
        
        if (VerifyActivationOrder())
        {
            if (clips[1])
            {
                if (!clips[1].isPlaying)
                {
                    clips[1].Play();
                }
            }

            StartCoroutine(Restore());
        }
    }

    private IEnumerator Restore()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < activationOrder.Length; i++)
        {
            activationOrder[i] = 0;
            transform.GetChild(i).GetComponent<Animator>().SetBool("Activate", false);
            transform.GetChild(i).GetComponent<ActivateCrank>().canActivate = true;
        }
    }
}

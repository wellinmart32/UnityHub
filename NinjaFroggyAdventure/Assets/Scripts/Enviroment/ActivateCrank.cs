using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCrank : MonoBehaviour
{
    public int position;
    public bool canActivate = true;
    private CrankObserver crankObserver;

    private void Awake()
    {
        crankObserver = GetComponentInParent<CrankObserver>();
    }

    private void Update()
    {
        if (GetComponent<Animator>().GetBool("Activate") && canActivate)
        {
            InsertActivation();
            canActivate = false;
        }
    }

    // revisar si se requiere ingresar en clase UI
    private void InsertActivation()
    {
        if (GetComponent<Animator>().GetBool("Activate"))
        {
            for (int i = 0; i < crankObserver.activationOrder.Length; i++)
            {
                if (crankObserver.activationOrder[i] == 0)
                {
                    crankObserver.activationOrder[i] = position;
                    return;
                }
            }
        }
    }
}

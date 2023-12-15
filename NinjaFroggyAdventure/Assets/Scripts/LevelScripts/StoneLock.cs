using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLock : MonoBehaviour
{
    public GameObject[] panels;

    private void Update()
    {
        int count = 0;

        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].GetComponent<Animator>().GetBool("Activate"))
            {
                count++;
            }
        }
        
        if (count == 5)
        {
            Invoke("DestroyStone", 0.5f);
        }
    }

    private void DestroyStone()
    {
        Destroy(gameObject);
    }
}

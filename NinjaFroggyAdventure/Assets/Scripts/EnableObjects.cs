using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjects : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] disabledObjects;
    public bool canEnable = false;

    private void Update()
    {
        int totalDisabledEnemies = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                totalDisabledEnemies++;
            }
        }
        
        if (totalDisabledEnemies == enemies.Length && canEnable)
        {
            StartCoroutine(ActivateObjects());
            canEnable = false;
        }
    }

    private IEnumerator ActivateObjects()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < disabledObjects.Length; i++)
        {
            disabledObjects[i].SetActive(true);
        }
    }
}

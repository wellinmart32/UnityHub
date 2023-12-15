using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostGuardianLife : MonoBehaviour
{
    public int lifes = 2;
    public float immuneTime = 0.5f;
    private int startLifes;
    private bool isImmune;
    private GameObject playerSkills;

    private void Awake()
    {
        playerSkills = GameObject.Find("StandarInterface").GetComponent<Initialization>().playerSkills;
        startLifes = lifes;
    }

    public void Hit()
    {
        if (!isImmune)
        {
            isImmune = true;
            lifes -= 1;
            StartCoroutine(DisableImmunity());

            if (lifes <= 0)
            {
                RestartLifes();
                isImmune = false;
                playerSkills.GetComponent<FrostGuardian>().Destransformation();
            }
        }
    }

    private IEnumerator DisableImmunity()
    {
        yield return new WaitForSeconds(immuneTime);
        isImmune = false;
    }

    public void RestartLifes()
    {
        lifes = startLifes;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAI : MonoBehaviour
{
    public bool canAttack = true;
    public float attackWaitTime = 3;
    public Animator animator;
    public GameObject plantBullet;
    public Transform launchSpawnPoint;
    public float feintTime = 2f;
    private float waitTime = 3;

    void Start()
    {
        waitTime = attackWaitTime;
    }

    void Update()
    {
        if (canAttack)
        {
            if (waitTime <= 0)
            {
                waitTime = attackWaitTime;
                animator.Play("Attack");
                Invoke("LaunchBullet", 0.5f);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void LaunchBullet()
    {
        Instantiate(plantBullet, launchSpawnPoint.position, launchSpawnPoint.rotation);
    }

    public IEnumerator GotFeint()
    {
        canAttack = false;
        if (animator != null) animator.SetBool("Idle", false);
        yield return new WaitForSeconds(feintTime);
        canAttack = true;
        if (animator != null) animator.SetBool("Idle", true);
    }
}

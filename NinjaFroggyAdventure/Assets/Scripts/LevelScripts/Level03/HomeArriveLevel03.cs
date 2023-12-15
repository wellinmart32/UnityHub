using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeArriveLevel03 : MonoBehaviour
{
    public GameObject woman;
    public GameObject[] people;
    public GameObject dialogue;
    public GameObject gif;
    public bool gifRecived;

    void Update()
    {
        if (DisableEnemiesLevel03.disableEnemiesLevel03 != null)
        {
            int totalPeople = 0;

            if (DisableEnemiesLevel03.disableEnemiesLevel03.canDisableEnemies)
            {
                woman.SetActive(true);
            }
        

            for (int i = 0; i < people.Length; i++)
            {
                if (people[i].GetComponent<FollowPlayerLevel03>().isArrive)
                {
                    totalPeople++;
                }
            }

            if (totalPeople == people.Length && !gifRecived)
            {
                SendGif();

                for (int i = 0; i < people.Length; i++)
                {
                    people[i].GetComponent<FollowPlayerLevel03>().enabled = false;
                    people[i].GetComponent<Animator>().SetBool("Idle", true);
                }

                gifRecived = true;
            }

            if (gifRecived)
            {
                for (int i = 0; i < people.Length; i++)
                {
                    people[i].GetComponent<Animator>().SetBool("Idle", true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("People"))
        {
            collision.GetComponent<FollowPlayerLevel03>().isArrive = true;
        }
    }

    private void SendGif()
    {
        dialogue.SetActive(true);
        gif.SetActive(true);
    }
}

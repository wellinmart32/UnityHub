using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostPeopleLevel03 : MonoBehaviour
{
    public GameObject[] people;
    public GameObject[] trappedPeople;
    public ActivatePanel activatePanel;
    public BoxCollider2D boxCollider2D;
    public GameObject door;
    public GameObject groundLock;
    public GameObject dialogue;
    public HomeArriveLevel03 homeArriveLevel03;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level03A")
        {
            if (DisableEnemiesLevel03.disableEnemiesLevel03 != null)
            {
                if (DisableEnemiesLevel03.disableEnemiesLevel03.canDisableEnemies)
                {
                    DisableRockLock();
                }
            }
            
            if (activatePanel.animator.GetBool("Activate"))
            {
                dialogue.SetActive(true);
                EnableHostPeopleEvent();
            }

            if (GameObject.Find("ArrivePoint").GetComponent<HomeArriveLevel03>().gifRecived)
            {
                door.SetActive(false);
            }

            for (int i = 0; i < trappedPeople.Length; i++)
            {
                if (activatePanel.animator.GetBool("Activate"))
                {
                    trappedPeople[i].GetComponent<MoveAndDisappear>().canMove = true;
                }
            }

            if (homeArriveLevel03.gifRecived)
            {
                groundLock.SetActive(false);
            }
        }
    }

    private void DisableRockLock()
    {
        boxCollider2D.enabled = false;
    }

    public void EnableHostPeopleEvent()
    {
        for (int i = 0; i < people.Length; i++)
        {
            people[i].SetActive(true);
        }
    }
}

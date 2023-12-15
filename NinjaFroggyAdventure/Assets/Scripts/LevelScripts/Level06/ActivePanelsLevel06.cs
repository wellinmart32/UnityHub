using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivePanelsLevel06 : MonoBehaviour
{
    public int[] panelListState;
    private bool canInit;
    private bool canLoadState;
    private GameObject[] panelList;
    private static ActivePanelsLevel06 activePanelsLevel06;

    private void Awake()
    {
        canInit = true;

        if (ActivePanelsLevel06.activePanelsLevel06 == null)
        {
            ActivePanelsLevel06.activePanelsLevel06 = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level06A" && canInit)
        {
            SetStates();
        }

        if (SceneManager.GetActiveScene().name == "Level06B")
        {
            canLoadState = true;
            canInit = false;
        }

        if (SceneManager.GetActiveScene().name == "Level06A" && canLoadState)
        {
            LoadPanelStates();
        }
    }

    private void SetStates()
    {
        GameObject panelController = GameObject.Find("PanelController");
        panelList = new GameObject[panelController.transform.childCount];
        panelListState = new int[panelController.transform.childCount];

        for (int i = 0; i < panelController.transform.childCount; i++)
        {
            panelList[i] = GameObject.Find("PanelController").transform.GetChild(i).gameObject;

            if (panelList[i].GetComponent<Animator>().GetBool("Activate") == false)
            {
                panelListState[i] = 0;
            }
            else
            {
                panelListState[i] = 1;
            }
        }
    }

    private void LoadPanelStates()
    {
        GameObject panelController = GameObject.Find("PanelController");

        for (int i = 0; i < panelController.transform.childCount; i++)
        {
            panelList[i] = GameObject.Find("PanelController").transform.GetChild(i).gameObject;

            if (panelListState[i] == 0)
            {
                panelList[i].GetComponent<Animator>().SetBool("Activate", false);
            }
            else
            {
                panelList[i].GetComponent<Animator>().SetBool("Activate", true);
                panelList[i].GetComponent<ActivatePanel>().SwitchVisibility();
            }
        }

        canLoadState = false;
        canInit = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour
{
    private GameObject merchantStore;
    [SerializeField] private Button storeButon;

    private void Awake()
    {
        merchantStore = GameObject.Find("StandarInterface").GetComponent<Initialization>().merchantStore;
        merchantStore.transform.GetChild(0).gameObject.SetActive(false);
        storeButon.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            storeButon.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            storeButon.gameObject.SetActive(false);
        }
    }
}

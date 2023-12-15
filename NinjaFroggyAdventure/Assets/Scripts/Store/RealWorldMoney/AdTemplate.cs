using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AdTemplate : MonoBehaviour
{
    public int idPos;
    public Image image;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI tittle;
    public int amount;

    private void Awake()
    {
        amountText.text = "X" + amount.ToString();
    }
}

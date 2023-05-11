using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankUI : MonoBehaviour
{
    public static BankUI Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private TextMeshProUGUI text;

    public void ChangeValue(float value)
    {
        string txt = MoneyAmountConvertator.IntoText(value);
        text.text = txt + "$/s";
    }
}

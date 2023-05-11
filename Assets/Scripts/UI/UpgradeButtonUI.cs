using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButtonUI : MonoBehaviour
{
    protected virtual int Progress { get; }
    protected virtual int MaxProgress { get; }
    protected virtual float CostOfProgress { get; }

    [Space]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    [Space]
    [SerializeField] private Image image;
    [SerializeField] private Sprite availableSpr, unavailableSpr;

    [Space]
    [SerializeField] private TextMeshProUGUI progressText;
    

    void Start()
    {
        if(Progress == MaxProgress) Off();
    }

    void Update()
    {
        if(CostOfProgress > Statistics.Money)
        {
            image.sprite = unavailableSpr;
            button.interactable = false;
        }
        else
        {
            image.sprite = availableSpr;
            button.interactable = true;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        string txt = MoneyAmountConvertator.IntoText(CostOfProgress);
        buttonText.text = txt + "$";

        if(progressText != null) progressText.text = $"{Progress}";
    }

    public void Off()
    {
        this.enabled = false;

        image.sprite = unavailableSpr;
        button.interactable = false;
        buttonText.text = "MAX";

        if(progressText != null) progressText.text = $"{Progress}";
    }
}

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
    protected virtual TargetPointer Pointer { get; }

    [Space]
    [SerializeField] private Button button;
    public TextMeshProUGUI buttonText;

    [Space]
    [SerializeField] private Image image;
    [SerializeField] private Sprite availableSpr, unavailableSpr;

    [Space]
    public TextMeshProUGUI progressText;
    
    void OnEnable()
    {
        /* if(Progress == MaxProgress) Off(); */
        /* transform.parent.eulerAngles = Camera.main.transform.eulerAngles; */
    }

    protected virtual bool AdditionalCondition{ get; }

    /* void Update()
    {
        Refresh();
    } */

    public virtual void OnButtonClick() {}

    public void Refresh()
    {
        UpdateText();

        if(Pointer != null) 
        {
            if(Progress > 0 || CostOfProgress > Statistics.Money || !AdditionalCondition)
            {
                Pointer.enabled = false;
            }
            else if(Progress <= 0 && CostOfProgress <= Statistics.Money && AdditionalCondition)
            {
                if(ProgressInListCondition) Pointer.enabled = true;
                else Pointer.enabled = false;
            }
        }

        if(Progress == MaxProgress) 
        {
            Off();
            return;
        }

        if(CostOfProgress <= Statistics.Money && AdditionalCondition)
        {
            image.sprite = availableSpr;
            button.interactable = true;
        }
        else
        {
            image.sprite = unavailableSpr;
            button.interactable = false;
        }
    }

    protected virtual bool ProgressInListCondition { get { return true; } }

    public virtual void UpdateText() {}

    public void Off()
    {
        this.enabled = false;

        image.sprite = unavailableSpr;
        button.interactable = false;
        buttonText.text = "MAX";

        if(Pointer != null) Pointer.enabled = false;

        if(progressText != null) progressText.text = $"{Progress}";
    }
}

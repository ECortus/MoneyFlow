using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChelickFlowUpgradeButtonUI : UpgradeButtonUI
{
    [Space]
    [SerializeField] private ChelickFlow flow;

    protected override int Progress { get => flow.Progress; }
    protected override int MaxProgress { get => flow.MaxProgress; }
    protected override float CostOfProgress { get => flow.CostOfProgress; }
    protected override bool AdditionalCondition { get => true; }

    [SerializeField] private TextMeshProUGUI count;

    void OnEnable()
    {
        transform.parent.eulerAngles = Camera.main.transform.eulerAngles;
    }

    public override void OnButtonClick()
    {
        flow.AddProgress();
    }

    public override void UpdateText()
    {
        base.UpdateText();

        string txt = MoneyAmountConvertator.IntoText(CostOfProgress);
        buttonText.text = txt + "$";

        txt = MoneyAmountConvertator.IntoText(flow.AllChelicksCount);
        count.text = txt;

        /* if(progressText != null) progressText.text = $"Lvl {Progress}"; */
    }
}

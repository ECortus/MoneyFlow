using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstructionUpgradeButtonUI : UpgradeButtonUI
{
    private Construction _construction;
    private Construction construction
    {
        get
        {
            if(_construction == null) _construction = GetComponentInParent<Construction>();
            return _construction;
        }
    }

    protected override int Progress { get => construction.Progress; }
    protected override int MaxProgress { get => construction.MaxProgress; }
    protected override float CostOfProgress { get => construction.CostOfProgress; }
    protected override bool AdditionalCondition 
    {
        get
        {
            bool value = true;
            if(construction.Progress > -1)
            {
                return value;
            }

            int index = LevelManager.Instance.ActualLevel.GetIndexOfConstruction(construction);
            List<Construction> constr = LevelManager.Instance.ActualLevel.Constructions;

            for(int i = 0; i < index; i++)
            {
                if(constr[i].Progress < 0)
                {
                    value = false;
                    break;
                }
            }

            return value;
        }
    }
    protected override TargetPointer Pointer => construction.pointer;
    protected override bool ProgressInListCondition
    {
        get
        {
            int index = LevelManager.Instance.ActualLevel.GetIndexOfConstruction(construction);
            return index < 3 * (Road.Instance.Size + 1);
        }
    }

    [Space]
    [SerializeField] private TextMeshProUGUI incomePerText;

    void OnEnable()
    {
        transform.eulerAngles = Camera.main.transform.eulerAngles;
    }

    public override void OnButtonClick()
    {
        construction.Upgrade();
    }

    public override void UpdateText()
    {
        base.UpdateText();

        string txt = MoneyAmountConvertator.IntoText(CostOfProgress);
        buttonText.text = txt + "$";

        if(incomePerText != null)
        {
            txt = MoneyAmountConvertator.IntoText(construction.IncomePerSecond);
            incomePerText.text = txt + "$";
        }

        if(progressText != null) progressText.text = $"{Progress}";
    }
}

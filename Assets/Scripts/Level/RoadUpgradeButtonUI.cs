using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoadUpgradeButtonUI : UpgradeButtonUI
{
    [Space]
    [SerializeField] private Road road;

    protected override int Progress { get => road.Size; }
    protected override int MaxProgress { get => road.MaxSize; }
    protected override float CostOfProgress { get => road.CostOfProgress; }

    public override void OnButtonClick()
    {
        road.Upgrade();
    }

    public override void UpdateText()
    {
        base.UpdateText();

        string txt = MoneyAmountConvertator.IntoText(CostOfProgress);
        buttonText.text = txt + "$";
    }

    protected override bool AdditionalCondition 
    { 
        get 
        {
            bool value = true;
            /* int count = (Progress + 1) * 3;

            List<Construction> constructions = LevelManager.Instance.ActualLevel.Constructions;
            
            for(int i = 0; i < count; i++)
            {
                if(constructions[i].Progress < 1)
                {
                    value = false;
                    break;
                }
            }
 */
            return value;
        }
    }
}

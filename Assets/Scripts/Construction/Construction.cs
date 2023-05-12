using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class Construction : ConstructionData
{
    [Space]
    [SerializeField] private ConstructionVisiters visit;
    [SerializeField] private PlusMoneyTextUI anim;

    [Header("UI par-s:")]
    [SerializeField] private ConstructionUpgradeButtonUI button;
    
    public void SetData()
    {
        /* await UniTask.WaitUntil(() => Bank.Instance); */

        Bank.Instance.AddConstruction(this);
        construction = this;

        ActivateMoneyAnim();

        button.enabled = true;

        foreach(Animation anim in Tiers)
        {
            anim.transform.localScale = Vector3.zero;
        }
    }

    public void ActivateMoneyAnim()
    {
        if(anim != null) anim.Activate(this);
    }

    public void CallToStore(Chelick chelick)
    {
        visit.Call(chelick);
    }

    public void Upgrade()
    {
        if(Progress == MaxProgress)
        {
            return;
        }

        Money.Minus(CostOfProgress);
        AddProgress();
        Reset();

        /* ConstructionSaving.SaveConstructions(); */
    }

    public void Reset()
    {
        if(Progress < setupProgress) 
        {
            Progress = setupProgress;
            /* ConstructionSaving.SaveConstructions(); */
        }

        Bank.Instance.UpdateAmountPerSecond();

        if(Progress > 0)
        {
            if(!buyed) Buy();
            ChangeAppearance();
        }
        else
        {
            Sell();
        }

        if(Progress == MaxProgress)
        {
            button.Off();
        }
        /* else
        {
            button.UpdateText();
        } */

        if(LevelManager.Instance.ActualLevel.TaskConditionComplete)
        {
            anim.Disable();
            LevelManager.Instance.EndLevel();
        }
    }
}

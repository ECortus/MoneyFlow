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

        /* Bank.Instance.AddConstruction(this); */
        Setup();
    }

    public void Setup()
    {
        construction = this;

        ActivateMoneyAnim();

        button.enabled = true;

        foreach(Animation anim in Tiers)
        {
            anim.transform.localScale = Vector3.zero;
        }
    }

    public void ResetVisiters()
    {
        visit.Visiters.Clear();
    }

    public void AnimMoneyIncome()
    {
        if(anim != null) anim.AnimIncome();
    }

    void ActivateMoneyAnim()
    {
        if(anim != null) anim.Activate(this);
    }

    void DisableMoneyAnim()
    {
        if(anim != null) anim.Disable();
    }

    public void CallToStore(Chelick chelick)
    {
        visit.Call(chelick);
    }

    public void Upgrade()
    {
        if(Progress == MaxProgress)
        {
            button.Off();
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

        /* Bank.Instance.UpdateAmountPerSecond(); */

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
            DisableMoneyAnim();
            LevelManager.Instance.EndLevel();
        }
    }
}

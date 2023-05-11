using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank Instance { get; set; }
    void Awake() => Instance = this;

    private float AmountPerSecond = 0f;
    private List<ConstructionData> constructions = new List<ConstructionData>();

    Coroutine coroutine;

    public void AddConstruction(ConstructionData data)
    {
        constructions.Add(data);
        UpdateAmountPerSecond();
    }

    public void RemoveConstruction(ConstructionData data)
    {
        constructions.Remove(data);
        UpdateAmountPerSecond();
    }

    public void AddBackgroundIncome()
    {
        
    }

    public void StartIncome()
    {
        foreach(Construction con in LevelManager.Instance.ActualLevel.Constructions)
        {
            con.SetData();
        }
        UpdateAmountPerSecond();

        if(coroutine == null) coroutine = StartCoroutine(Income());
    }

    public void StopIncome()
    {
        constructions.Clear();
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Income()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while(true)
        {
            yield return wait;
            AddMoney();
        }
    }

    void AddMoney()
    {
        Money.Plus(AmountPerSecond);

        /* foreach(Construction con in constructions)
        {
            con.ActivateMoneyAnim();
        } */
    }

    public void UpdateAmountPerSecond()
    {
        AmountPerSecond = 0f;
        foreach(ConstructionData construction in constructions)
        {
            AmountPerSecond += construction.IncomePerSecond;
        }

        AmountPerSecond *= ChelickFlow.Instance.incomePlusPercent;

        BankUI.Instance.ChangeValue(AmountPerSecond);
    }
}

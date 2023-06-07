using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChelickFlow : MonoBehaviour
{
    public static ChelickFlow Instance { get; set; }
    void Awake() => Instance = this;

    public int Progress
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.FlowKey, 0);
        }

        set
        {
            PlayerPrefs.SetInt(DataManager.FlowKey, value);
            PlayerPrefs.Save();
        }
    }

    [Header("Progress: ")]
    public int MaxProgress;

    [Header("Info: ")]
    [SerializeField] private float defaultIncomeUpPerProgress = 1.4f;
    [SerializeField] private float incomeModPerLevel = 0.4f;

    [Header("Cost: ")]
    [SerializeField] private float costOfProgressDefault = 7f;
    [SerializeField] private float costModPerLevel = 1.6f;

    [Header("More chelicks...")]
    [SerializeField] private int plusChelicks = 10;
    public int RequiredPlusChelicks => plusChelicks * Progress;

    public int AllChelicksCount
    {
        get
        {
            int def = LevelManager.Instance.ActualLevel.defaultChelickCount;
            int plus = ChelickFlow.Instance.RequiredPlusChelicks;

            return def + plus;
        }
    }

    [Space]
    [SerializeField] private ChelickFlowUpgradeButtonUI button;

    void Start()
    {
        Reset();
    }

    public float IncomePlusPercent
    {
        get
        {
            float value = 1f + defaultIncomeUpPerProgress + incomeModPerLevel * Progress;
            return value;
        }
    }

    public float CostOfProgress
    {
        get
        {
            float value = costOfProgressDefault * Mathf.Pow(costModPerLevel, Progress);
            return value;
        }
    }

    public void AddProgress()
    {
        if(Progress == MaxProgress)
        {
            button.Off();
            return;
        }

        Money.Minus(CostOfProgress);

        int value = Progress + 1;
        Progress = value;

        /* Bank.Instance.UpdateAmountPerSecond(); */
        Reset();

        LevelManager.Instance.ActualLevel.RefreshAllButtons();
    }

    public void ResetToDefault()
    {
        Progress = 0;
        Reset();
    }

    public void Reset()
    {
        if(Progress == MaxProgress)
        {
            button.Off();
        }
        /* else
        {
            button.UpdateText();
        } */
    }
}

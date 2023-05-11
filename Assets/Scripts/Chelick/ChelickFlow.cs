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
    private float incomePlusPercentDefault = 1f;
    [Range(0, 1)]
    [SerializeField] private float incomeUpPerProgress;

    [Header("Cost: ")]
    [SerializeField] private float costOfProgressDefault;
    [SerializeField] private float costUpPerProgress;

    [Header("More chelicks...")]
    [SerializeField] private int plusChelicks = 10;
    public int RequiredPlusChelicks => plusChelicks * Progress;

    [Space]
    [SerializeField] private ChelickFlowUpgradeButtonUI button;

    void Start()
    {
        Reset();
    }

    public float incomePlusPercent
    {
        get
        {
            float value = incomePlusPercentDefault + Progress * incomeUpPerProgress;
            return value;
        }
    }

    public float CostOfProgress
    {
        get
        {
            float value = costOfProgressDefault + Progress * costUpPerProgress;
            return value;
        }
    }

    public void AddProgress()
    {
        int value = Progress + 1;
        Progress = value;

        Money.Minus(CostOfProgress);

        Bank.Instance.UpdateAmountPerSecond();
        Reset();
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

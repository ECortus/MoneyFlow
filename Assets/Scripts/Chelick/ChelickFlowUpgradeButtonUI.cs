using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChelickFlowUpgradeButtonUI : UpgradeButtonUI
{
    [Space]
    [SerializeField] private ChelickFlow flow;

    protected override int Progress { get => flow.Progress; }
    protected override int MaxProgress { get => flow.MaxProgress; }
    protected override int CostOfProgress { get => (int)flow.CostOfProgress; }

    void OnEnable()
    {
        transform.parent.eulerAngles = Camera.main.transform.eulerAngles;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstructionUpgradeButtonUI : UpgradeButtonUI
{
    [Space]
    [SerializeField] private Construction construction;

    protected override int Progress { get => construction.Progress; }
    protected override int MaxProgress { get => construction.MaxProgress; }
    protected override int CostOfProgress { get => (int)construction.CostOfProgress; }

    void OnEnable()
    {
        transform.eulerAngles = Camera.main.transform.eulerAngles;
    }
}

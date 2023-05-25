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
    protected override int CostOfProgress { get => (int)road.CostOfProgress; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class Level : MonoBehaviour
{
    public void On() => gameObject.SetActive(true);
    public void Off() => gameObject.SetActive(false);
    public void Eliminate() => Destroy(gameObject);

    [Header("Chelicks par-s: ")]
    public int defaultChelickCount = 1;
    public int moneyOnStart = 50;

    /* [Header("Construction par-s: ")] */
    /* [HideInInspector]  */public List<Construction> Constructions = new List<Construction>();
    public int GetIndexOfConstruction(Construction con)
    {
        return Constructions.IndexOf(con);
    }
    public Road Road;
    [HideInInspector] public ChelickFlow Flow;

    [Header("Camera par-s: ")]
    public float leftBound;
    [SerializeField] private List<int> MaxRightBoundByRoad;

    public float rightBound
    {
        get
        {
            return MaxRightBoundByRoad[/* Road.Instance.Size */MaxRightBoundByRoad.Count - 1];
        }
    }

    [Space]
    [SerializeField] private List<UpgradeButtonUI> AllUpgradeButtons = new List<UpgradeButtonUI>();

    public void StartLevel()
    {
        /* Constructions = GetComponentsInChildren<Construction>().ToList(); */

        Bank.Instance.StartIncome();
        ConstructionSaving.LoadConstructions();

        /* VisitSimulation.Instance.StartSim(); */

        CameraController.Instance.Reset();
        GameManager.Instance.SetActive(true);

        /* ChelicksSpawner.Instance.StartSpawner(); */
        ChelickGenerator.Instance.DeleteAll();

        RefreshAllButtons();
    }

    public void EndLevel()
    {
        UI.Instance.EndLevel();

        ChelicksSpawner.Instance.StopSpawner();
        VisitSimulation.Instance.StopSim();

        Bank.Instance.StopIncome();

        GameManager.Instance.SetActive(false);
    }

    public void ResetToDefaultLevel()
    {
        /* Constructions = GetComponentsInChildren<Construction>().ToList(); */

        ChelickGenerator.Instance.DeleteAll();

        ConstructionSaving.ResetToDefaultConstructions();
        Road.ResetToDefault();

        Flow = Road.transform.GetComponentInChildren<ChelickFlow>();
        Flow.ResetToDefault();

        Money.Minus(Statistics.Money);
        MoneyUI.Instance.ResetMoney();

        Money.Plus(moneyOnStart);
    }

    public void RefreshAllButtons()
    {
        foreach(UpgradeButtonUI button in AllUpgradeButtons)
        {
            /* if(button.gameObject.activeInHierarchy)  */button.Refresh();
        }
    }

    public bool TaskConditionComplete
    {
        get
        {
            foreach(Construction construction in Constructions)
            {
                if(construction.MaxProgress != construction.Progress) return false;
            }

            return true;
        }
    }
} 

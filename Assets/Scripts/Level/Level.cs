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
    public float moneyOnStart = 50f;

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

    private int boundIndex
    {
        get
        {
            int t = 0;
            List<Construction> constructions = LevelManager.Instance.ActualLevel.Constructions;

            if(constructions[constructions.Count - 1].Progress > 0) return MaxRightBoundByRoad.Count - 1;
            
            for(int i = 0; i < constructions.Count; i++)
            {
                if(constructions[i].Progress < 1)
                {
                    break;
                }
                
                if((i + 1) % 3 == 0) t++;
            }
            return t;
        }
    }

    public float rightBound
    {
        get
        {
            return MaxRightBoundByRoad[boundIndex];
        }
    }

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

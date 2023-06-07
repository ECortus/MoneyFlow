using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionVisiters : MonoBehaviour
{
    [SerializeField] private Construction construction;

    [Space]
    [SerializeField] private Transform exit;
    private Vector3 enter 
    {
        get
        {
            return exit.position + new Vector3(0f, 0f, 1f) * 1f;
        }
    }

    private float minTime = 2f;
    private float maxTime = 12f;

    public List<Visiter> Visiters = new List<Visiter>();

    int roadLevel => Road.Instance.Size;
    float maxDelay = 2f;
    float time = 2f;
    int previousRoadLevel;

    void Start()
    {
        previousRoadLevel = roadLevel;
    }

    void Update()
    {
        if(Visiters.Count == 0 || !GameManager.Instance.isActive) return;

        if(roadLevel != previousRoadLevel)
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                time = maxDelay;
                previousRoadLevel = roadLevel;
            }
            return;
        }

        for(int i = 0; i < Visiters.Count; i++)
        {
            Visiters[i].Time -= Time.deltaTime;
            if(Visiters[i].Time < 0f)
            {
                Remove(Visiters[i]);
            }
        }
    }

    public void Call(Chelick chelick)
    {
        chelick.SetTarget(enter, true);
    }

    void Add(Chelick chelick)
    {
        chelick.Off();

        Visiter visiter = new Visiter();
        visiter.Data = chelick;
        visiter.Time = Random.Range(minTime, maxTime);
        Visiters.Add(visiter);
    }

    void Remove(Visiter visiter)
    {
        /* Vector3 target = Road.Instance.GetRandomPointOnZ(transform); */
        Vector3 target = Road.Instance.RandomPointBehindSpawner;

        Money.Plus(construction.IncomePerSecond);
        construction.AnimMoneyIncome();

        Visiters.Remove(visiter);
        ChelickGenerator.Instance.Spawn(exit.position, -Vector3.forward, visiter.Data, target);
        visiter.Data.bag.On();

        int value = Random.Range(0, 100);
        if(value < 50)
        {
            Road.Instance.CallToRandomStall(visiter.Data);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Chelick")
        {
            Add(col.GetComponent<Chelick>());
        }
    }
}

[System.Serializable]
public class Visiter
{
    public float Time;
    public Chelick Data;
}

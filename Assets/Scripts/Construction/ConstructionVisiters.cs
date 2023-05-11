using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionVisiters : MonoBehaviour
{
    [SerializeField] private Transform exit;
    private Vector3 enter 
    {
        get
        {
            return exit.position + new Vector3(0f, 0f, 1f) * 1f;
        }
    }

    private float minTime = 2f;
    private float maxTime = 15f;

    public List<Visiter> Visiters = new List<Visiter>();

    void Update()
    {
        if(Visiters.Count == 0) return;

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
        Visiter visiter = new Visiter();
        visiter.Data = chelick;
        visiter.Time = Random.Range(minTime, maxTime);
        Visiters.Add(visiter);

        chelick.Off();
    }

    void Remove(Visiter visiter)
    {
        Vector3 target = Road.Instance.GetRandomPointOnZ(transform);

        Visiters.Remove(visiter);
        ChelickGenerator.Instance.Spawn(exit.position, visiter.Data, target);
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

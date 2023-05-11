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

    /* [Header("Construction par-s: ")] */
    [HideInInspector] public List<Construction> Constructions = new List<Construction>();

    [Header("Camera par-s: ")]
    public float leftBound;
    public float rightBound;

    public void StartLevel()
    {
        Constructions = GetComponentsInChildren<Construction>().ToList();

        /* ChelickGenerator.Instance.SpawnCount(defaultChelickCount); */
        Bank.Instance.StartIncome();
        ConstructionSaving.LoadConstructions();

        ChelicksSpawner.Instance.StartSpawner();

        GameManager.Instance.SetActive(true);
    }

    public void EndLevel()
    {

    }
} 

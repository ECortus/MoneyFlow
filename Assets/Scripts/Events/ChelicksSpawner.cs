using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChelicksSpawner : MonoBehaviour
{
    public static ChelicksSpawner Instance { get; set; }
    void Awake() => Instance = this;

    [Header("Par-s: ")]
    [SerializeField] private float delay;
    [SerializeField] private int minCount, maxCount;

    private int roadMod => (Road.Instance.Size + 1);

    WaitForSeconds wait => new WaitForSeconds(delay / roadMod * Road.Instance.SpawnCount);

    private int CurrentCount
    {
        get
        {
            return ChelickGenerator.Instance.Count;
        }
    }

    private int RequiredCount
    {
        get
        {
            int def = LevelManager.Instance.ActualLevel.defaultChelickCount;
            int plus = ChelickFlow.Instance.RequiredPlusChelicks * roadMod;

            return def + plus;
        }
    }

    Coroutine coroutine;

    void OnDisable()
    {
        StopSpawner();
    }

    public void StartSpawner()
    {
        if(coroutine == null && gameObject.activeInHierarchy) coroutine = StartCoroutine(Work());
    }

    public void StopSpawner()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Work()
    {
        yield return new WaitForSeconds(3f);

        int count = 0;

        while(true)
        {
            if(!GameManager.Instance.isActive)
            {
                coroutine = null;
                break;
            }

            if(RequiredCount > CurrentCount)
            {
                count = Random.Range(minCount * roadMod, maxCount * roadMod);
                count = Mathf.Clamp(count, 0, RequiredCount);

                for(int i = 0; i < count; i++)
                {
                    ChelickGenerator.Instance.Spawn(Road.Instance.RandomPoint);
                }

                VisitSimulation.Instance.DistributeChelicks(count);

                yield return wait;
            }

            yield return null;
        }
    }
}

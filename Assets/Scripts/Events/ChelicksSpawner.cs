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

    public void StartSpawner()
    {
        if(coroutine == null) coroutine = StartCoroutine(Work());
    }

    public void StopSpawner()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Work()
    {
        WaitForSeconds wait = new WaitForSeconds(delay / roadMod);

        int count = 0;

        while(true)
        {
            if(RequiredCount > CurrentCount)
            {
                count = Random.Range(minCount * roadMod, maxCount * roadMod);
                count = Mathf.Clamp(count, 0, RequiredCount);

                for(int i = 0; i < count; i++)
                {
                    ChelickGenerator.Instance.Spawn(Road.Instance.RandomPoint);
                }

                yield return wait;
            }

            yield return null;
        }
    }
}

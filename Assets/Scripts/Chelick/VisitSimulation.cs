using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitSimulation : MonoBehaviour
{
    public static VisitSimulation Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private float delayBetweenCall = 1f;
    private WaitForSeconds wait => new WaitForSeconds(delayBetweenCall);

    private List<Construction> Constructions => LevelManager.Instance.ActualLevel.Constructions;
    private List<Chelick> Chelicks => ChelickGenerator.Instance.List;

    private int SpawnNowCount = 0;
    public void SetSpawnNowCount(int count)
    {
        SpawnNowCount = count;
    }

    Coroutine coroutine;

    /* void Start()
    {
        StartSim();
    } */

    public void StartSim()
    {
        if(coroutine == null) coroutine = StartCoroutine(Sim());
    }

    public void StopSim()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Sim()
    {
        int indexCo;
        int callCount;
        Construction construction;
        Chelick chelick;

        yield return wait;

        while(true)
        {
            if(Chelicks.Count > 0)
            {
                indexCo = Random.Range(0, Constructions.Count);
                construction = Constructions[indexCo];

                callCount = Constructions[indexCo].Progress;
                if(callCount > 1) callCount /= 2;
                else if (callCount == 0) continue;

                if(SpawnNowCount > 0)
                {
                    callCount = 1;
                }

                for(int i = 0; i < callCount; i++)
                {
                    chelick = GetChelickBehind(construction);
                    if(chelick == null)
                    {
                        continue;
                    }

                    construction.CallToStore(chelick);
                }

                if(SpawnNowCount > 0)
                {
                    yield return new WaitForSeconds(0.5f);
                    SpawnNowCount -= callCount;
                }
                else
                {
                    yield return wait;
                }
            }

            yield return null;
        }
    }

    Chelick GetChelickBehind(Construction con)
    {
        Chelick chel;
        int i = Chelicks.Count;

        while(i > 0)
        {
            chel = Chelicks[Random.Range(0, Chelicks.Count)];
            if(con.transform.position.x > chel.transform.position.x && !chel.called)
            {
                return chel;
            }

            i--;
        }

        return null;
    }
}

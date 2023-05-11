using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitSimulation : MonoBehaviour
{
    [SerializeField] private float delayBetweenCall = 1f;
    private WaitForSeconds wait => new WaitForSeconds(delayBetweenCall);

    private List<Construction> Constructions => LevelManager.Instance.ActualLevel.Constructions;
    private List<Chelick> Chelicks => ChelickGenerator.Instance.List;

    Coroutine coroutine;

    void Start()
    {
        StartSim();
    }

    void StartSim()
    {
        if(coroutine == null) coroutine = StartCoroutine(Sim());
    }

    void StopSim()
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

                for(int i = 0; i < callCount; i++)
                {
                    chelick = GetChelickBehind(construction);
                    if(chelick == null)
                    {
                        continue;
                    }

                    construction.CallToStore(chelick);
                }

                yield return wait;
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

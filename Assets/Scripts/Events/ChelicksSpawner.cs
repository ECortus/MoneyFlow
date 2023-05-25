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

    public Vector3 Direction
    {
        get
        {
            return transform.forward;
        }
    }

    public Vector3 RandomPoint
    {
        get
        {
            Vector3 main = transform.position;
            Vector3 scale = transform.localScale;

            main += new Vector3(
                Random.Range(-scale.x / 2 + Road.Instance.boundX, scale.x / 2 - Road.Instance.boundX),
                -0.5f,
                Random.Range(-scale.z / 2 + Road.Instance.boundZ, scale.z / 2 - Road.Instance.boundZ)
            );

            return main;
        }
    }

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
        if(coroutine == null && gameObject.activeInHierarchy && this.enabled) coroutine = StartCoroutine(Work());
    }

    public void StopSpawner()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Work()
    {
        if(!Tutorial.Instance.Complete)
        {   
            yield return new WaitUntil(() => Tutorial.Instance.Complete);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }

        int count = 0;

        Chelick chel;
        List<Chelick> chelicks = new List<Chelick>();

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

                chelicks.Clear();

                for(int i = 0; i < count; i++)
                {
                    chel = ChelickGenerator.Instance.Spawn(RandomPoint, Direction);
                    chelicks.Add(chel);
                }

                VisitSimulation.Instance.DistributeChelicks(count, chelicks);

                yield return wait;
            }

            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChelickGenerator : MonoBehaviour
{
    public static ChelickGenerator Instance { get; set; }
    void Awake() => Instance = this;

    public int Count => List.Count;
    [SerializeField] private List<GameObject> ChelicksPrefabs = new List<GameObject>();

    [HideInInspector] public List<Chelick> List = new List<Chelick>();

    public void AddChelick(Chelick chelick)
    {
        List.Add(chelick);
    }

    public void RemoveChelick(Chelick chelick)
    {
        List.Remove(chelick);
        /* ObjectPool.Instance.Add(ObjectType.Chelick, chelick.gameObject); */
    }

    /* public void SpawnCount(int count, Vector3 pos, Vector3 rot = new Vector3())
    {
        for(int i = 0; i < count; i++)
        {
            Spawn(pos, rot);
        }
    }

    public void DeleteCount(int count)
    {
        int value = 0;
        if(List.Count < count) value = List.Count;
        else value = count;

        for(int i = 0; i < value; i++)
        {
            Delete();
        }
    }

    public void SetChelickToTargetCount(int count, Transform target)
    {
        int value = 0;
        if(List.Count < count) value = List.Count;
        else value = count;

        for(int i = 0; i < value; i++)
        {
            SetChelickToTarget(target);
        }
    } */

    public void DeleteAll()
    {
        int count = List.Count;
        for(int i = 0; i < count; i++)
        {
            ObjectPool.Instance.Add(ObjectType.Chelick, List[0].gameObject);
            List[0].Off();
        }
    }


    public void Spawn(Vector3 pos, Chelick chelick = null, Vector3 target = new Vector3())
    {
        if(chelick != null)
        {
            chelick.transform.position = pos;
            if(target != new Vector3()) chelick.SetTarget(target);

            /* ObjectPool.Instance.Delete(ObjectType.Chelick, chelick.gameObject); */
            chelick.On();
            return;
        }

        GameObject prefab = ChelicksPrefabs[Random.Range(0, ChelicksPrefabs.Count)];
        Chelick chel = ObjectPool.Instance.Insert(ObjectType.Chelick, prefab, pos).GetComponent<Chelick>();

        if(target != new Vector3()) chel.SetTarget(target);
    }

    public void Delete()
    {
        Chelick obj = List[Random.Range(0, List.Count - 1)];
        obj.Off();
        ObjectPool.Instance.Add(ObjectType.Chelick, obj.gameObject);
    }

    public void SetChelickToTarget(Vector3 target)
    {
        int index = Random.Range(0, List.Count - 1);
        List[index].SetTarget(target);
    }
}

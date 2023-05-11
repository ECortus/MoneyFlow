using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    void Awake() => Instance = this;

    private List<GameObject> ChelickPool = new List<GameObject>();

    public GameObject Insert(ObjectType type, GameObject obj, Vector3 pos = new Vector3(), Vector3 rot = new Vector3())
    {
        switch(type)
        {
            case ObjectType.Chelick:
                GameObject go = null;

                if(ChelickPool.Count > 0)
                {
                    go = ChelickPool[Random.Range(0, ChelickPool.Count - 1)];
                    ChelickPool.Remove(go);
                    go.transform.position = pos;
                    go.transform.eulerAngles = rot;
                }
                else
                {
                    go = Instantiate(obj, pos, Quaternion.Euler(rot));
                }

                if(go != null) 
                {
                    go.GetComponent<Chelick>().On();
                    return go;
                }
                break;
            default:
                break;
        }

        return null;
    }

    public void Add(ObjectType type, GameObject obj)
    {
        switch(type)
        {
            case ObjectType.Chelick:
                ChelickPool.Add(obj);
                break;
            default:
                break;
        }
    }

    public void Delete(ObjectType type, GameObject obj)
    {
        switch(type)
        {
            case ObjectType.Chelick:
                if(ChelickPool.Contains(obj)) ChelickPool.Remove(obj);
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
public enum ObjectType
{
    Default, Chelick
}

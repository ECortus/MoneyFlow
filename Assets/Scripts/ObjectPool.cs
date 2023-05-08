using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    void Awake() => Instance = this;

    public GameObject Insert(ObjectType type, GameObject obj, Vector3 pos, Vector3 rot)
    {
        switch(type)
        {
            default:
                return null;
        }
    }
}

[System.Serializable]
public enum ObjectType
{
    Default
}

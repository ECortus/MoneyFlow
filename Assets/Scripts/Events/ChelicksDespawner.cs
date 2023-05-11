using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChelicksDespawner : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Chelick")
        {
            col.GetComponent<Chelick>().Off();
            ObjectPool.Instance.Add(ObjectType.Chelick, col.gameObject);
        }
    }
}

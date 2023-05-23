using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChelicksDespawner : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Chelick chel;

        if(col.tag == "Chelick")
        {
            chel = col.GetComponent<Chelick>();
            chel.Off();
            ObjectPool.Instance.Add(ObjectType.Chelick, col.gameObject);
        }
    }
}

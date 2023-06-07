using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChelicksDespawner : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        if(!gameObject.activeInHierarchy) return;

        Chelick chel;

        if(col.tag == "Chelick")
        {
            chel = col.GetComponent<Chelick>();

            if(!chel.gameObject.activeSelf) return;

            chel.Off();
            ObjectPool.Instance.Add(ObjectType.Chelick, col.gameObject);
        }
    }
}

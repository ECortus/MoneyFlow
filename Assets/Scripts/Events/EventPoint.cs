using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventPoint : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;

    void OnTriggerEnter(Collider col)
    {
        
    }
}

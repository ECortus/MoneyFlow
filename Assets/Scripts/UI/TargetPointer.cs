using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour 
{
    public GameObject pointerPrefab;
    public bool HideOnDistance;

    private void OnEnable() 
    {
        PointerManager.Instance.AddToList(this);
    }
    
    public void OnDisable() 
    {
        PointerManager.Instance.RemoveFromList(this);
    }

}

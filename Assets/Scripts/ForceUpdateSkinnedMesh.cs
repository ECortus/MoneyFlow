using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUpdateSkinnedMesh : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    /* private SkinnedMeshRenderer _skin;
    private SkinnedMeshRenderer skinnedMesh
    {
        get
        {
            if(_skin == null) _skin = GetComponent<SkinnedMeshRenderer>();
            return _skin;
        }
    }

    void Update()
    {
        if(skinnedMesh.isVisible)
        {
            skinnedMesh.enabled = false;
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    void OnRenderObject()
    {
        if(!skinnedMesh.enabled)
        {
            skinnedMesh.enabled = true;
            canvas.SetActive(true);
        }
    } */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionsGrid : MonoBehaviour
{
    [SerializeField] private float spaceBetween;

    private List<Transform> List
    {
        get
        {
            List<Transform> list = new List<Transform>();
            foreach(Transform child in transform)
            {
                list.Add(child);
            }
            return list;
        }
    }

    private float space = 0f;
    private Vector3 position;

    [ContextMenu("Build")]
    private void Build()
    {
        space = 0f;

        foreach(Transform construction in List)
        {
            if(construction == null) continue;

            position = construction.localPosition;
            position.x = space;
            construction.localPosition = position;

            space += spaceBetween;
        }
    }
}

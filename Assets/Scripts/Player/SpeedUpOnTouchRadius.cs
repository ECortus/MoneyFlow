using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpOnTouchRadius : MonoBehaviour
{
    [SerializeField] private SpeedUpSphere sphere;

    [Space]
    [SerializeField] private float radiusOnSpeedUp;
    [SerializeField] private float speedUpScale;

    [Space]
    [SerializeField] private LayerMask roadMask;

    SpeedUpSphere Current;

    Ray ray;
    RaycastHit hit;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, roadMask))
            {
                CreateSpeedUpSphere(hit.point);
            }
        }

        if(Input.GetMouseButton(0) && Current != null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, roadMask))
            {
                MoveSphere(hit.point);
            }
            else
            {
                DeleteSpeedUpSphere();
            }
        }

        if(Input.GetMouseButtonUp(0) && Current != null)
        {
            DeleteSpeedUpSphere();
        }
    }

    public void CreateSpeedUpSphere(Vector3 point)
    {
        if(Current == null)
        {
            Current = Instantiate(sphere);
        }

        Current.On(point, radiusOnSpeedUp, speedUpScale);
    }

    public void MoveSphere(Vector3 point)
    {
        Current.UpdatePos(point);
    }

    public void DeleteSpeedUpSphere()
    {
        if(Current.gameObject.activeSelf) Current.Off();
    }
}

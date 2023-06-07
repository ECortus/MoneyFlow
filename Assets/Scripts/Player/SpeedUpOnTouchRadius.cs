using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SpeedUpOnTouchRadius : MonoBehaviour
{
    [SerializeField] private SpeedUpSphere sphere;

    [Space]
    [SerializeField] private float radiusOnSpeedUp;
    [SerializeField] private float speedUpScale;
    [SerializeField] private float timeAlive;

    [Space]
    [SerializeField] private LayerMask roadMask;

    SpeedUpSphere Current;

    Ray ray;
    RaycastHit hit;

    float time = 0f;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !PointerIsOverUI.Instance.CheckThis())
        {
            TryCreate();
        }

        if(Current != null)
        {
            if(Current.gameObject.activeSelf)
            {
                time += Time.deltaTime;
                if(time >= timeAlive)
                {
                    DeleteSpeedUpSphere();
                    Current = null;
                }
            }
        }

        /* if(Input.GetMouseButton(0) && Current != null)
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
        } */
    }

    async void TryCreate()
    {
        await UniTask.Delay(100);

        if((Current == null || !Current.gameObject.activeSelf) 
            && Mathf.Abs(CameraController.Instance.DiffMouseSplit) < 0.05f)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, roadMask))
            {
                CreateSpeedUpSphere(hit.point);
                time = 0f;
            }
        }
    }

    public void CreateSpeedUpSphere(Vector3 point)
    {
        if(Current == null)
        {
            Current = Instantiate(sphere);
        }

        Current.On(point, radiusOnSpeedUp, speedUpScale);

        if(!Tutorial.Instance.ACCELERATION_isDone)
        {
            Tutorial.Instance.ACCELERATION_isDone = true;
            Tutorial.Instance.SetState(TutorialState.NONE);
        }
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

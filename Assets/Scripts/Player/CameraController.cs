using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity, speed = 100f;

    private float mouseStartX, mouseStartY, diffMouseX, diffMouseY, lastX;

    private float leftBound => LevelManager.Instance.ActualLevel.leftBound;
    private float rightBound => LevelManager.Instance.ActualLevel.rightBound;

    private float diffMouseSplit
    {
        get
        {
            return diffMouseX + diffMouseY;
        }
    }
    private Vector3 pos
    {
        get
        {
            float X = lastX + diffMouseSplit;
            X = Mathf.Clamp(X, leftBound, rightBound);

            return new Vector3(
                X,
                transform.position.y,
                transform.position.z
            );
        }
    }

    private Transform target;
    public void SetTarget(Transform trg)
    {
        target = trg;
    }
    public void ResetTarget()
    {
        target = null;
    }

    void Update()
    {
        if(!GameManager.Instance.isActive) return;

        if(Input.GetMouseButtonDown(0))
        {
            PrepareToMove();
        }

        if(Input.GetMouseButton(0) || transform.position != pos)
        {
            Move();
        }

        if(Input.GetMouseButtonUp(0))
        {

        }
    }

    void PrepareToMove()
    {
        mouseStartX = Input.mousePosition.x;
        mouseStartY = Input.mousePosition.y;

        lastX = transform.position.x;

        diffMouseX = 0f;
        diffMouseY = 0f;
    }

    void Move()
    {
        Vector3 point;

        if(Input.GetMouseButton(0))
        {
            diffMouseX = (mouseStartX - Input.mousePosition.x) / Screen.width * sensitivity;
            diffMouseY = -(mouseStartY - Input.mousePosition.y) / Screen.height * sensitivity;
        }

        if(target == null)
        {
            point = pos;
        }
        else
        {
            point = target.position;
        }

        transform.position = Vector3.Lerp(transform.position, point, speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private float MouseZoomSpeed = 15.0f;
    [SerializeField] private float TouchZoomSpeed = 0.1f;
    [SerializeField] private float ZoomMinBound = 0.1f;
    [SerializeField] private float ZoomMaxBound = 179.9f;

    void Update()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 2)
            {
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);

                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance (tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance (tZero.position, tOne.position);

                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(Mathf.Abs(scroll) > 0.05f) Zoom(scroll, MouseZoomSpeed);
        }

        cam.transform.localPosition = new Vector3(
            Mathf.Clamp(cam.transform.localPosition.x, min.x, max.x),
            Mathf.Clamp(cam.transform.localPosition.y, min.y, max.y),
            Mathf.Clamp(cam.transform.localPosition.z, max.z, min.z)
        );

        /* if(cam.fieldOfView < ZoomMinBound) 
        {
            cam.fieldOfView = ZoomMinBound;
        }
        else if(cam.fieldOfView > ZoomMaxBound) 
        {
            cam.fieldOfView = ZoomMaxBound;
        } */
    }

    Vector3 dir => cam.transform.TransformDirection(Vector3.forward);

    Vector3 min => dir * ZoomMinBound;
    Vector3 max => -dir * ZoomMaxBound;

    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        Vector3 direction = dir;
        cam.transform.localPosition -= direction * deltaMagnitudeDiff * speed;

        cam.transform.localPosition = new Vector3(
            Mathf.Clamp(cam.transform.localPosition.x, min.x, max.x),
            Mathf.Clamp(cam.transform.localPosition.y, min.y, max.y),
            Mathf.Clamp(cam.transform.localPosition.z, max.z, min.z)
        );

        /* cam.fieldOfView += deltaMagnitudeDiff * speed;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, ZoomMinBound, ZoomMaxBound); */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSphere : MonoBehaviour
{
    private float maxUpScale;
    private float upRadius;

    public float Scale
    {
        get
        {
            return maxUpScale /* * (time / maxTime) */;
        }
    }

    public void On(Vector3 pos, float radius, float maxScale)
    {
        gameObject.SetActive(true);

        upRadius = radius;
        maxUpScale = maxScale;

        transform.position = pos;
        transform.localScale = Vector3.one * radius;
        
        time = 0f;
    }

    public void UpdatePos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    float time;
    float maxTime = 0.5f;

    void Update()
    {
        if(time < maxTime) time += Time.deltaTime * 3f;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, upRadius);
    }
}

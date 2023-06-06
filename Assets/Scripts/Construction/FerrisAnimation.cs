using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisAnimation : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> cabines;
    Coroutine coroutine;

    void OnEnable()
    {
        if(coroutine == null) coroutine = StartCoroutine(Anim());
    }

    void OnDisable()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Anim()
    {
        WaitForSeconds wait = new WaitForSeconds(Time.deltaTime);

        while(true)
        {
            wheel.Rotate(new Vector3(0f, 0f, speed * Time.deltaTime), Space.World);
            foreach(Transform cabin in cabines)
            {
                cabin.eulerAngles = new Vector3(-90f, -90f, 90f);
            }

            yield return wait;

            if(transform.lossyScale.x < 0.05f)
            {
                yield return new WaitUntil(() => transform.lossyScale.x > 0.05f);
            }
        }
    }
}

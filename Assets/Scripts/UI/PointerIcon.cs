using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerIcon : MonoBehaviour 
{
    [SerializeField] private float showSpeed;
    public bool isShown = true;

    private void Start() 
    {
        isShown = false;
        transform.localScale = Vector3.zero;
    }

    public void SetIconPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Show()
    {
        if (isShown) return;

        isShown = true;
        StopAllCoroutines();
        StartCoroutine(ShowProcess());
    }

    public void Hide() 
    {
        if (!isShown) return;

        isShown = false;
        StopAllCoroutines();
        StartCoroutine(HideProcess());
    }

    IEnumerator ShowProcess() 
    {
        transform.localScale = Vector3.zero;

        while(transform.localScale.x < 1f)
        {
            transform.localScale += new Vector3(
                showSpeed * Time.deltaTime, showSpeed * Time.deltaTime, showSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.localScale = Vector3.one;
    }

    IEnumerator HideProcess() 
    {
        while(transform.localScale.x > 0f)
        {
            transform.localScale -= new Vector3(
                showSpeed * Time.deltaTime, showSpeed * Time.deltaTime, showSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.localScale = Vector3.zero;
    }
}

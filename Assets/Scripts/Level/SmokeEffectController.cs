using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float delayToOff, delayToOn;
    Coroutine coroutine;

    void OnEnable()
    {
        StartPlay();
    }

    void OnDisable()
    {
        StopPlay();
    }

    public void StartPlay()
    {
        if(coroutine == null) coroutine = StartCoroutine(Play());
    }

    public void StopPlay()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Play()
    {
        while(true)
        {
            effect.Play();
            yield return new WaitForSeconds(delayToOff);

            effect.Stop();
            yield return new WaitForSeconds(delayToOn);
        }
    }
}

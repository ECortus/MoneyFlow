using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particles;
    [SerializeField] private float delayToOn;

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
        WaitForSeconds on = new WaitForSeconds(delayToOn);
        ParticleSystem currentParticle = null;

        while(true)
        {
            if(currentParticle != null) currentParticle.Stop();

            currentParticle = particles[Random.Range(0, particles.Count - 1)];
            if(currentParticle != null)
            {
                currentParticle.Play();
            }

            yield return on;
        }
    }
}

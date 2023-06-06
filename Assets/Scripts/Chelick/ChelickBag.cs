using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChelickBag : MonoBehaviour
{
    public bool Active = false;
    [SerializeField] private Transform bagParent;
    [SerializeField] private List<GameObject> buyingEffects = new List<GameObject>();
    private GameObject BuyingEffect => buyingEffects[Random.Range(0, buyingEffects.Count)];

    void Start()
    {
        Off();
    }

    public void On()
    {
        bagParent.gameObject.SetActive(true);
        Active = true;

        GameObject effect;
        if(buyingEffects.Count > 0) 
        {
            effect = BuyingEffect;
            if(effect == null) return;

            GameObject go = ParticlePool.Instance.Insert(ParticleType.ChelickEmoji, effect, transform.position + Vector3.up * 2f);
            go.transform.parent = transform;
        }
    }

    public void Off()
    {
        bagParent.gameObject.SetActive(false);
        Active = false;
    }
}

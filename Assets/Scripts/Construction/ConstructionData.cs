using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConstructionData : MonoBehaviour
{
    [HideInInspector] public Construction construction;

    private int Index
    {
        get
        {
            return ConstructionSaving.GetIndexByData(construction);
        }
    }
    
    [Header("Setup par-s: ")]
    public int setupProgress;

    public int Progress
    {
        get
        {
            return ConstructionSaving.LoadConstruction(Index);
        }

        set
        {
            ConstructionSaving.SaveConstruction(value, Index);
        }
    }

    [Header("Progress: ")]
    public int MaxProgress;
    public int progressStep;

    [Header("Objects: ")]
    public List<Animation> Tiers;
    [SerializeField] private GameObject defaultObj, buyedObj;

    [Header("Info: ")]
    [SerializeField] private float incomePerSecondDefault;
    [SerializeField] private float incomeUpPerProgress;
    [SerializeField] private float costOfProgressDefault;
    [SerializeField] private float costUpPerProgress;

    [HideInInspector] public bool buyed = false;

    [SerializeField] private GameObject upgradeEffect;

    public float IncomePerSecond
    {
        get
        {
            float value = (Progress > 1 ? incomePerSecondDefault : 0) + Progress * incomeUpPerProgress;
            return value;
        }
    }

    public float CostOfProgress
    {
        get
        {
            float value = costOfProgressDefault + Progress * (Progress / 2f) * costUpPerProgress;
            return value;
        }
    }

    public void AddProgress()
    {
        int value = Progress + 1;
        Progress = value;
    }

    public void Buy()
    {
        /* if(buyed) return; */

        defaultObj.SetActive(false);
        buyedObj.SetActive(true);

        buyed = true;
    }

    public void Sell()
    {
        /* if(!buyed) return; */

        defaultObj.SetActive(true);
        buyedObj.SetActive(false);

        buyed = false;
    }

    public void ChangeAppearance()
    {
        int variant = (Progress / progressStep);

        if(variant > Tiers.Count - 1) 
        {
            variant = Tiers.Count - 1;
        }
        
        for(int i = 0; i < Tiers.Count; i++)
        {
            if(i == variant)
            {
                if(Tiers[i].transform.localScale.x < 1f)
                {
                    Tiers[i].Play("ShowConstruction");
                    ParticlePool.Instance.Insert(ParticleType.UpgradeContruction, upgradeEffect, transform.position);
                }
            }
            else 
            {
                if(Tiers[i].transform.localScale.x > 0f)
                {
                    Tiers[i].Play("HideConstruction");
                }
            }
        }
    }
}
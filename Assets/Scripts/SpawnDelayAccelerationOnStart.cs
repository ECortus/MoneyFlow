using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDelayAccelerationOnStart : MonoBehaviour
{
    private bool Complete
    {
        get
        {
            return PlayerPrefs.GetInt("BonusAccelerationDelay", 0) == 1;
        }
        set
        {
            int val = value ? 1 : 0;
            PlayerPrefs.SetInt("BonusAccelerationDelay", val);
            PlayerPrefs.Save();
        }
    }

    [SerializeField] private float bonusScale = 5f, timeOfStartBonus = 10f;
    [SerializeField] private ChelicksSpawner spawner;
    float time = 0f;

    void Start()
    {
        if(Complete)
        {
            Destroy(this);
            return;
        }

        spawner.SetAdditionalMod(bonusScale);
        Complete = true;
    }
    
    void Update()
    {
        if(!Tutorial.Instance.Complete) return;

        if(time < timeOfStartBonus)
        {
            time += Time.deltaTime;
        }
        else
        {
            spawner.ResetAdditionalMod();
            Destroy(this);
        }
    }
}

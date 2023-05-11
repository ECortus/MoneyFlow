using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics
{
    public static int LevelIndex
    {
        get
        {
            int lvl = PlayerPrefs.GetInt(DataManager.LevelIndexKey, 0);
            return lvl;
        }

        set
        {
            int lvl = value;
            PlayerPrefs.SetInt(DataManager.LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }

    private static float _money;
    public static float Money
    {
        get
        {
            float amount = PlayerPrefs.GetFloat(DataManager.MoneyKey, 0);
            return amount;
        }

        set
        {
            float amount = value;
            PlayerPrefs.SetFloat(DataManager.MoneyKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int RoadSize
    {
        get
        {
            int lvl = PlayerPrefs.GetInt(DataManager.RoadKey, 0);
            return lvl;
        }

        set
        {
            int lvl = value;
            PlayerPrefs.SetInt(DataManager.RoadKey, value);
            PlayerPrefs.Save();
        }
    }
}

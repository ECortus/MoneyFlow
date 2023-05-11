using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static string TutorialKey = "TutorialComplete";

    public static string LevelIndexKey = "LevelIndex";

    public static string MoneyKey = "Money";

    public static string FlowKey = "Flow";
    public static string RoadKey = "Road";

    public static void Save()
    {

    }

    public static void Load()
    {
        
    }

    public static void Reset()
    {
        Statistics.RoadSize = 0;
    }
}

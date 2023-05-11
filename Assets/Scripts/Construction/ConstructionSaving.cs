using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstructionSaving
{
    private static List<Construction> List => LevelManager.Instance.ActualLevel.Constructions;

    private static string baseName = "Construction";

    public static int GetIndexByData(Construction construction)
    {
        return List.IndexOf(construction);
    }

    public static string GetName(int index)
    {
        return $"{baseName}{index}";
    }

    public static void SaveConstruction(int progress, int index)
    {
        PlayerPrefs.SetInt($"{baseName}{index}", progress);
        PlayerPrefs.Save();
    }

    public static int LoadConstruction(int index)
    {
        return PlayerPrefs.GetInt($"{baseName}{index}", 0);
        /* List[index].Reset(); */
    }

    public static void SaveConstructions()
    {
        for(int i = 0; i < List.Count; i++)
        {
            SaveConstruction(List[i].Progress, i);
        }
        PlayerPrefs.Save();
    }

    public static void LoadConstructions()
    {
        for(int i = 0; i < List.Count; i++)
        {
            List[i].Progress = LoadConstruction(i);
            List[i].Reset();
        }
    }

    public static void ResetConstructions()
    {
        for(int i = 0; i < List.Count; i++)
        {
            PlayerPrefs.SetInt($"{baseName}{i}", 0);
            List[i].Progress = 0;
            List[i].Reset();
        }
        PlayerPrefs.Save();
    }
}

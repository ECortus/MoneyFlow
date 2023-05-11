using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI Instance { get; set; }

    [SerializeField] private EndLevelUI End;

    void Awake()
    {
        Instance = this;
    }

    public void On()
    {

    }

    public void Off()
    {

    }
    public void Restart()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
        End.Close();
    }

    public void EndLevel()
    {
        End.Open();
    }
}
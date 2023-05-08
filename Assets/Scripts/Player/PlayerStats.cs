using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; set; }

    private bool _active;
    public bool Active { get { return _active; }}
    public void On()
    {
        _active = true;

        UI.Instance.On();
    }
    public void Off()
    {
        _active = false;

        UI.Instance.Off();
    }

    void Awake() => Instance = this;

    void OnEnable()
    {

    }
}

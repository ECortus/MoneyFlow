using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalls : MonoBehaviour
{
    public static Stalls Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private List<AdditionalStall> stalls = new List<AdditionalStall>();

    void OnDisable()
    {
        Destroy(this);
    }

    public void CallToRandomStall(Chelick chel)
    {
        stalls[Random.Range(0, stalls.Count)].Call(chel);
    }

    public void CallToStall(Chelick chel, int index)
    {
        stalls[index].Call(chel);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public static Road Instance { get; set; }
    void Awake() => Instance = this;

    [Header("Progress: ")]
    public int MaxSize;
    public int Size { get { return Statistics.RoadSize; } set { Statistics.RoadSize = value; } }

    [Space]
    [SerializeField] private float boundX;
    [SerializeField] private float boundZ;

    [Header("Upgrade parameters: ")]
    [SerializeField] private float plusScaleSize = 1f;

    [Header("Cost: ")]
    [SerializeField] private float costDefault;
    [SerializeField] private float costPerProgress;

    [Space]
    [SerializeField] private RoadUpgradeButtonUI button;

    public float CostOfProgress
    {
        get
        {
            float value = costDefault + Size * costPerProgress;
            return value;
        }
    }

    [Space]
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform spawner, flow;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        defaultScale = transform.localScale;
        defaultPos = transform.position;
        defaultCanvasPos = canvas.position;
        defaultFlowPos = spawner.position;
        defaultFlowScale = spawner.localScale;

        ResetRoad(Size);
    }

    private Vector3 defaultScale, defaultPos, defaultCanvasPos, defaultFlowPos, defaultFlowScale;

    public Vector3 RandomDirection
    {
        get
        {
            /* int value = Random.Range(0, 2);
            Vector3 direction = value > 0 ? transform.right : -transform.right;
            return direction; */
            return transform.right;
        }
    }

    public Vector3 RandomPoint
    {
        get
        {
            Vector3 main = spawner.position;
            Vector3 scale = spawner.localScale;

            main += new Vector3(
                Random.Range(-scale.x / 2 + boundX, scale.x / 2 - boundX),
                -0.5f,
                Random.Range(-scale.z / 2 + boundZ, scale.z / 2 - boundZ)
            );

            return main;
        }
    }

    public Vector3 GetRandomPointOnZ(Transform ax)
    {
        Vector3 main = RandomPoint;
        main.x = ax.position.x;

        return main;
    }

    public void Upgrade()
    {
        int value = Size + 1;
        Size = value;

        ResetRoad(value);
    }

    public void ResetRoad(int lvl)
    {
        transform.position = defaultPos - new Vector3(
            0f,
            0f,
            plusScaleSize / 2f * lvl
        );

        transform.localScale = defaultScale + new Vector3(
            0f,
            0f,
            plusScaleSize * lvl
        );

        canvas.position = defaultCanvasPos - new Vector3(
            0f,
            0f,
            plusScaleSize * lvl
        );

        flow.position = defaultFlowPos - new Vector3(
            0f,
            0f,
            plusScaleSize / 2f * lvl
        );

        spawner.localScale = defaultFlowScale + new Vector3(
            0f,
            0f,
            plusScaleSize * lvl
        );

        if(Size == MaxSize)
        {
            button.Off();
        }
        /* else
        {
            button.UpdateText();
        } */
    }
}

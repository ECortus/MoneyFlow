using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    /* [Header("Upgrade parameters: ")]
    [SerializeField] private float plusScaleSize = 1f; */

    [Header("Cost: ")]
    [SerializeField] private float costDefault;
    [SerializeField] private float costPerProgress;

    [Space]
    [SerializeField] private RoadUpgradeButtonUI button;

    public float CostOfProgress
    {
        get
        {
            float value = costDefault + Size * Size * costPerProgress;
            return value;
        }
    }

    /* [Space]
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform flow; */
    private List<ChelicksSpawner> spawners;

    [Space]
    [SerializeField] private List<Animation> Tiers;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        foreach(Animation anim in Tiers)
        {
            anim.transform.localScale = Vector3.zero;
        }

        /* defaultScale = transform.localScale;
        defaultPos = transform.position;
        defaultCanvasPos = canvas.position;
        defaultFlowPos = flow.position;
        
        defaultSpawnersScale.Clear();
        foreach(Transform spawner in spawners)
        {   
            defaultSpawnersScale.Add(spawner.localScale);
        } */

        ResetRoad();
    }

    /* private Vector3 defaultScale, defaultPos, defaultCanvasPos, defaultFlowPos;
    private List<Vector3> defaultSpawnersScale; */

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
            Transform spawner = spawners[Random.Range(0, spawners.Count)].transform;

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

        Money.Minus(CostOfProgress);

        ResetRoad();
    }

    void ChangeAppearance()
    {
        int variant = Size;
        ChelickGenerator.Instance.DeleteAll();
        /* VisitSimulation.Instance.SetSpawnNowCount(10); */

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
                }

                spawners = Tiers[i].transform.GetComponentsInChildren<ChelicksSpawner>().ToList();
                button = Tiers[i].transform.GetComponentInChildren<RoadUpgradeButtonUI>();
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

    public void ResetToDefault()
    {
        Size = 0;

        Setup();
        ResetRoad();

        foreach(Animation anim in Tiers)
        {
            anim.transform.localScale = Vector3.zero;
        }
    }

    public void ResetRoad()
    {
        ChangeAppearance();

        /* transform.position = defaultPos - new Vector3(
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

        for(int i = 0; i < spawners.Count; i++)
        {   
            spawners[i].localScale = defaultSpawnersScale[i] + new Vector3(
                0f,
                0f,
                plusScaleSize * lvl
            );
        } */

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;

public class Road : MonoBehaviour
{
    public static Road Instance { get; set; }
    void Awake() => Instance = this;

    [Header("Size: ")]
    [SerializeField] private int setupSize = 0;
    public int MaxSize;
    public int Size { get { return Statistics.RoadSize; } set { Statistics.RoadSize = value; } }

    [Space]
    public float boundX;
    public float boundZ;

    /* [Header("Upgrade parameters: ")]
    [SerializeField] private float plusScaleSize = 1f; */

    [Header("Cost: ")]
    [SerializeField] private List<float> Costs;
    /* [SerializeField] private float costDefault;
    [SerializeField] private float costPerSize; */

    [Space]
    [SerializeField] private RoadUpgradeButtonUI button;

    public float CostOfProgress
    {
        get
        {
            if(Size + 1 > Costs.Count - 1) return 0f;

            float value = /* costDefault + Size * (Size / 2f) * costPerSize; */Costs[Size + 1];
            return value;
        }
    }

    /* [Space]
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform flow; */
    public int SpawnCount => spawners.Count;
    private List<ChelicksSpawner> spawners = new List<ChelicksSpawner>();
    private List<SeatPlace> seatPlaces = new List<SeatPlace>();
    private List<AdditionalStall> stalls = new List<AdditionalStall>();

    [Space]
    [SerializeField] private List<Animation> Tiers = new List<Animation>();
    [SerializeField] private List<NavMeshSurface> surfaces = new List<NavMeshSurface>();

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

    public void CallToRandomStall(Chelick chel)
    {
        if(stalls.Count > 0) stalls[Random.Range(0, stalls.Count)].Call(chel);
    }

    public void CallToStall(Chelick chel, int index)
    {
        stalls[index].Call(chel);
    }

    public Vector3 MainDirection
    {
        get
        {
            return transform.right;
        }
    }

    public Vector3 RandomDirection
    {
        get
        {
            ChelicksSpawner spawner = spawners[Random.Range(0, spawners.Count)];
            return spawner.Direction;
        }
    }

    public Vector3 RandomPointOnSpawner
    {
        get
        {
            ChelicksSpawner spawner = spawners[Random.Range(0, spawners.Count)];
            return spawner.RandomPoint;
        }
    }

    public SeatPlace RandomSeat
    {
        get
        {
            if(seatPlaces.Count == 0) return null;

            SeatPlace seat = seatPlaces[Random.Range(0, seatPlaces.Count)];
            return seat.Free ? seat : null;
        }
    }

    public Vector3 RandomPointBehindSpawner
    {
        get
        {
            ChelicksSpawner spawner = spawners[Random.Range(0, spawners.Count)];
            bool right = Random.Range(0, 100) > 50 ? true : false;
            
            return spawner.RandomPoint + spawner.Direction * 
                (spawners.IndexOf(spawner) < 2 ? (right ? 250f : -15f) : -5f);
        }
    }

    public Vector3 RandomPoint
    {
        get
        {
            int index = spawners.Count > 1 ? Random.Range(0, 2) : 0;
            ChelicksSpawner spawner = spawners[index];

            Vector3 main = spawner.transform.position;
            Vector3 scale = spawner.transform.localScale;

            main += new Vector3(
                Random.Range(-scale.x / 2 + boundX, scale.x / 2 - boundX),
                -0.5f,
                Random.Range(-scale.z / 2 + boundZ, scale.z / 2 - boundZ)
            );

            return main;
        }
    }

    public Vector3 GetRandomPointOnZ(Vector3 ax)
    {
        Vector3 main = RandomPoint;
        main.x = ax.x;

        return main;
    }

    public void Upgrade()
    {
        if(Size == MaxSize)
        {
            button.Off();
            return;
        }

        Money.Minus(CostOfProgress);

        int value = Size + 1;
        Size = value;

        ResetRoad();
        LevelManager.Instance.ActualLevel.RefreshAllButtons();
    }

    async void ChangeAppearance()
    {
        int variant = Size;
        ChelickGenerator.Instance.DeleteAll();
        /* VisitSimulation.Instance.SetSpawnNowCount(10); */
        if(SpawnCount > 0)
        {
            foreach(ChelicksSpawner spawner in spawners)
            {
                spawner.StopSpawner();
                spawner.enabled = false;
            }
        }

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
                    Tiers[i].gameObject.SetActive(true);
                    Tiers[i].Play("ShowConstruction");
                }
                surfaces[i].enabled = true;

                spawners = Tiers[i].transform.GetComponentsInChildren<ChelicksSpawner>().ToList();
                seatPlaces = Tiers[i].transform.GetComponentsInChildren<SeatPlace>().ToList();
                stalls = Tiers[i].transform.GetComponentsInChildren<AdditionalStall>().ToList();
                button = Tiers[i].transform.GetComponentInChildren<RoadUpgradeButtonUI>();
            }
            else 
            {
                if(Tiers[i].transform.localScale.x > 0f)
                {
                    Tiers[i].Play("HideConstruction");
                    /* Tiers[i].transform.localScale = Vector3.zero;
                    Tiers[i].gameObject.SetActive(false); */
                }
                surfaces[i].enabled = false;
            }
        }

        await UniTask.Delay(1500);
        foreach(ChelicksSpawner spawner in spawners)
        {
            spawner.enabled = true;
            spawner.StartSpawner();
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
        if(Size != setupSize && setupSize != 0) 
        {
            Size = setupSize;
            /* ConstructionSaving.SaveConstructions(); */
        }

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

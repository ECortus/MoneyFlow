using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; set; }

    [SerializeField] private HandShowHide MOVE, UPGRADE;
    public bool MOVE_isDone, UPGRADE_isDone;

    public bool Complete
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.TutorialKey, 0) == 1;
        }
        set
        {
            int val = value ? 1 : 0;
            PlayerPrefs.SetInt(DataManager.TutorialKey, val);
            PlayerPrefs.Save();
            /* Condition(); */
        }
    }

    [Space]
    public TutorialState State = TutorialState.NONE;

    void Awake()
    {   
        Instance = this;
    }

    void Start()
    {
        if(Complete)
        {
            MOVE_isDone = true;
            UPGRADE_isDone = true;
            return;
        }

        if(!Complete && LevelManager.Instance.ActualLevel.Constructions[0].Progress == 0) 
        {
            SetState(TutorialState.UPGRADE, true);
        }
        else
        {
            Off();
        }
    }

    void Update()
    {
        if(Complete) return;
    }

    public void SetState(TutorialState _state, bool done = false)
    {
        if(_state == TutorialState.NONE) OffAll();

        if(_state == State) return;

        OffAll();
        State = _state;

        switch(State)
        {
            case TutorialState.MOVE:
                /* if(!MOVE_isDone)  */MOVE.Open();
                CameraController.Instance.Active = true;
                MOVE_isDone = done;
                break;
            case TutorialState.UPGRADE:
                /* if(!UPGRADE_isDone)  */UPGRADE.Open();
                CameraController.Instance.Active = false;
                UPGRADE_isDone = done;
                break;
            default:
                Debug.Log("looser");
                break;
        }
    }

    public void Off()
    {
        Complete = true;
        MOVE_isDone = true;
        UPGRADE_isDone = true;
        SetState(TutorialState.NONE);

        this.enabled = false;
        gameObject.SetActive(false);
    }

    void OffAll()
    {
        MOVE.Close();
        UPGRADE.Close();
    }

    public async void Condition()
    {
        if(Complete)
        {
            await UniTask.Delay(1500);
            gameObject.SetActive(false);
            Instance = null;
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

public enum TutorialState
{
    NONE, UPGRADE, MOVE
}

/* if(Tutorial.Instance != null)
            {
                if(!Tutorial.Instance.Complete)
                {
                    Tutorial.Instance.SetState(TutorialState.CHANGEPLAYTYPE);
                }
            } */

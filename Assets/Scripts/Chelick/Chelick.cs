using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chelick : MonoBehaviour
{
    /* public static Chelick Instance { get; set; }
    void Awake() => Instance = this; */

    private static readonly int _Speed = Animator.StringToHash("Speed");

    /* [SerializeField] private Animator animator; */
    [SerializeField] private Animation animations;
    public NavMeshAgent Agent;
    /* [SerializeField] private Rigidbody rb; */

    private float defspeed = 0f;
    private float speed => defspeed * (1 + mod);
    private float mod = 0f;

    [HideInInspector] public bool called = false;
    bool planned = false;

    public ChelickBag bag;

    public AdditionalPropsController props;
    private string propsName = "";
    public void Props(string Name = "")
    {
        propsName = Name;
        props.Refresh(Name);
    }
    private string WalkingAnimationName
    {
        get
        {
            if(propsName == "") return "Walking";
            else return "Walk&Eat";
        }
    }

    public Vector3 target;
    private float targetX => target.x;
    private float targetZ = -9999f;
    public void SetTarget(Vector3 trg, bool pl = false)
    {
        target = trg;
        targetZ = Road.Instance.GetRandomPointOnZ(trg).z;

        called = true;
        planned = pl;
    }
    public void ResetTarget()
    {
        called = false;
        planned = false;
        target = new Vector3();
        /* direction = MainDirection; */
    }

    private Vector3 rotateTarget;
    public void SetRotateTarget(Vector3 trg)
    {
        rotateTarget = trg;
    }
    public void ResetRotateTarget()
    {
        rotateTarget = new Vector3();
    }

    private Vector3 direction;
    private Vector3 MainDirection;

    private bool SpawnOnMain = true;

    public void On(Vector3 dir)
    {
        defspeed = Random.Range(2f, 2.5f);
        /* if(dir.x < Vector3.positiveInfinity.x) MainDirection = dir;

        if(dir.x < 0.05f)
        {
            SpawnOnMain = false;
        }
        else
        {
            SpawnOnMain = true;
        } */

        gameObject.SetActive(true);
        ChelickGenerator.Instance.AddChelick(this);

        int value = Random.Range(0, 100);
        if(value > 50)
        {
            StartCoroutine(GoToSeat());
            return;
        }

        animations.Play(WalkingAnimationName);
    }

    public void Off()
    {
        ResetTarget();
        gameObject.SetActive(false);
        ChelickGenerator.Instance.RemoveChelick(this);

        bag.Off();
        Props();

        animations.Stop();
    }

    bool goSeat = false;
    IEnumerator GoToSeat()
    { 
        goSeat = true;

        SeatPlace place = Road.Instance.RandomSeat;
        if(place != null)
        {
            place.Free = false;
            Vector3 point = place.Point;
            Agent.SetDestination(point);

            yield return new WaitUntil(() => Vector3.Distance(point, transform.position) < 0.2f);
            /* animations.Stop(); */
            animations.Play("Talking");
            SetRotateTarget(point + place.transform.forward);

            Agent.enabled = false;
            transform.position = transform.position - place.transform.forward * 0.85f;

            yield return new WaitForSeconds(10f);

            animations.Play(WalkingAnimationName);

            ResetRotateTarget();

            place.Free = true;
        }

        yield return null;
        Agent.enabled = true;
        goSeat = false;
    }

    void Update()
    {
        if(sphere != null)
        {
            if(!sphere.gameObject.activeSelf) mod = 0f;
        }
        else
        {
            mod = 0f;
        }
        
        Agent.speed = speed;

        if(!GameManager.Instance.isActive)
        {
            direction = Vector3.zero;
            animations.Stop();
            
            return;
        }

        /* if(target != null)
        {
            if(Vector3.Distance(target, transform.position) < 1f)
            {
                ResetTarget();
            }
        } */

        /* UpdateDirection(); */
        if(rotateTarget != new Vector3())
        {
            Agent.angularSpeed = 0f;
            Rotate();
        }
        else
        {
            Agent.angularSpeed = 540f;
        }

        Move();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        /* animator.SetFloat(_Speed, rb.velocity.magnitude); */
        if(Agent.velocity.magnitude < 0.1f && animations.isPlaying && !goSeat)
        {
            animations.Stop();
            return;
        }

        if(!animations.isPlaying) animations.Play(WalkingAnimationName);
    }

    private void UpdateDirection()
    {
        if(target != new Vector3())
        {
            if(planned)
            {
                if(Mathf.Abs(targetX - transform.position.x) > 1f && SpawnOnMain)
                {
                    direction = MainDirection;
                    return;
                }

                if(!SpawnOnMain)
                {
                    if(Mathf.Abs(targetZ + ((MainDirection.z) > 0f ? 1f : -1f) - transform.position.z) > 1f)
                    {
                        direction = MainDirection;
                        return;
                    }

                    if(Mathf.Abs(targetX /* - ((MainDirection.x) > 0f ? 1f : -1f) * ((MainDirection.z) > 0f ? 1f : 0f) */ - transform.position.x) > 0.5f)
                    {
                        direction = Mathf.Sign(targetX - transform.position.x) * Road.Instance.MainDirection;
                        return;
                    }
                }
            }
            direction = (target - transform.position).normalized;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void Move()
    {
        /* rb.velocity = direction * speed; */
        /* transform.position = Vector3.Lerp(transform.position, transform.position + direction * speed, speed / 4f * Time.deltaTime); */
        
        if(!goSeat) Agent.SetDestination(target);
    }

    private void Rotate()
    {
        var targetRotation = Quaternion.LookRotation((rotateTarget - Vector3.forward * 0.5f) - transform.position);
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360f);
    }

    SpeedUpSphere sphere;

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "SpeedUp")
        {
            if(sphere == null) sphere = col.GetComponent<SpeedUpSphere>();
            mod = sphere.Scale;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "SpeedUp")
        {
            sphere = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chelick : MonoBehaviour
{
    /* public static Chelick Instance { get; set; }
    void Awake() => Instance = this; */

    private static readonly int _Speed = Animator.StringToHash("Speed");

    /* [SerializeField] private Animator animator; */
    [SerializeField] private Animation walkingAnimation;
    [SerializeField] private Rigidbody rb;

    private float speed = 0f;

    [HideInInspector] public bool called = false;
    bool planned = false;

    public ChelickBag bag;

    private Vector3 target;
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

    private Vector3 direction;
    private Vector3 MainDirection;

    private bool SpawnOnMain = true;

    public void On(Vector3 dir)
    {
        speed = Random.Range(2.5f, 4f);
        if(dir.x < Vector3.positiveInfinity.x) MainDirection = dir;

        if(dir.x < 0.05f)
        {
            SpawnOnMain = false;
        }
        else
        {
            SpawnOnMain = true;
        }

        gameObject.SetActive(true);
        ChelickGenerator.Instance.AddChelick(this);

        walkingAnimation.Play();
    }

    public void Off()
    {
        ResetTarget();
        gameObject.SetActive(false);
        ChelickGenerator.Instance.RemoveChelick(this);

        bag.Off();

        walkingAnimation.Stop();
    }

    void Update()
    {
        if(!GameManager.Instance.isActive)
        {
            rb.velocity = Vector3.zero;
            walkingAnimation.Stop();
            
            return;
        }

        if(target != null)
        {
            if(Vector3.Distance(target, transform.position) < 1f)
            {
                ResetTarget();
            }
        }

        UpdateDirection();
        UpdateVelocity();
        Rotate();

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        /* animator.SetFloat(_Speed, rb.velocity.magnitude); */
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
            direction = MainDirection;
        }
    }

    private void UpdateVelocity()
    {
        rb.velocity = direction * speed;
    }

    private void Rotate()
    {
        var targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360f);
    }
}

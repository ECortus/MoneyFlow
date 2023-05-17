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

    [SerializeField] private float speed;

    [HideInInspector] public bool called = false;
    bool planned = false;

    public ChelickBag bag;

    private Vector3 target;
    public void SetTarget(Vector3 trg, bool pl = false)
    {
        called = true;
        planned = pl;
        target = trg;
    }
    public void ResetTarget()
    {
        called = false;
        planned = false;
        target = new Vector3();
        /* direction = Road.Instance.RandomDirection; */
    }

    private Vector3 direction;

    public void On()
    {
        gameObject.SetActive(true);
        ChelickGenerator.Instance.AddChelick(this);

        walkingAnimation.Play();
    }

    public void Off()
    {
        ResetTarget();
        gameObject.SetActive(false);
        ChelickGenerator.Instance.RemoveChelick(this);

        walkingAnimation.Stop();
    }

    void Update()
    {
        if(!GameManager.Instance.isActive)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if(target != null)
        {
            if(Vector3.Distance(target, transform.position) < 0.5f)
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
                /* if(Vector3.Distance(target, transform.position) > 10f) */
                if(Mathf.Abs(target.x - transform.position.x) > 1f)
                {
                    direction = Road.Instance.RandomDirection;
                    return;
                }
            }
            direction = (target - transform.position).normalized;
        }
        else
        {
            direction = Road.Instance.RandomDirection;
        }
    }

    private void UpdateVelocity()
    {
        rb.velocity = direction * speed;
    }

    public void Rotate()
    {
        var targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0f;
        targetRotation.z = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatPlace : MonoBehaviour
{
    public bool Free = true;
    public Vector3 Point => transform.position + transform.forward * 1f;
}

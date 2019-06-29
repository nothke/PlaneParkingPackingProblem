using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public Airplane airplane;

    public Rigidbody planeRb;
    public WheelCollider wheel;

    public static List<Connector> all = new List<Connector>();

    private void Start()
    {
        all.Add(this);
    }
}

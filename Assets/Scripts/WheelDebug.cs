using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelDebug : MonoBehaviour
{
    public WheelCollider wheel;

    public float motorTorque;
    public float brakeTorque;

    void Update()
    {
        motorTorque = wheel.motorTorque;
        brakeTorque = wheel.brakeTorque;
    }
}

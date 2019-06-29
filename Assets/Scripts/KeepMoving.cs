using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMoving : MonoBehaviour
{
    public WheelCollider[] wheel;

    void Update()
    {
        for (int i = 0; i < wheel.Length; i++)
        {
            wheel[i].motorTorque = 0.0001f;
        }
    }
}

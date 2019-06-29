using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public WheelCollider[] poweredWheels;
    public WheelCollider[] steeredWheels;

    public float maxAccelTorque = 10000;
    public float maxBrakeTorque = 10000;
    public float maxSteer = 30;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float accelInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");

        float accel = 0;
        float brake = 0;

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        float forwardVelo = localVelocity.z;

        if (forwardVelo < 0.01f && forwardVelo > -0.01f)
        {
            accel = accelInput;
        }
        else if (forwardVelo > 0.01f)
        {
            accel = Mathf.Clamp01(accelInput);
            brake = Mathf.Clamp01(-accelInput);
        }
        else
        {
            accel = Mathf.Clamp01(-accelInput);
            brake = Mathf.Clamp01(accelInput);
        }

        for (int i = 0; i < poweredWheels.Length; i++)
        {
            poweredWheels[i].motorTorque = accelInput * maxAccelTorque;
            poweredWheels[i].brakeTorque = brake * maxBrakeTorque;
        }

        for (int i = 0; i < steeredWheels.Length; i++)
        {
            steeredWheels[i].steerAngle = steerInput * maxSteer;
        }

        //Debug.DrawRay(transform.position, rb.velocity * 1, Color.yellow);
    }
}

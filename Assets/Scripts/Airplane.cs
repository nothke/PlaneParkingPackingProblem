using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public WheelCollider[] wheel;

    public float brakeTorque = 10000;
    public bool inProduction;

    bool braking = true;

    public float damageImpulseMult = 0.001f;

    public float totalDamage;

    Rigidbody _rb;
    public Rigidbody RB { get { if (!_rb) _rb = GetComponent<Rigidbody>(); return _rb; } }

    public void EngageBrakes(bool engage)
    {
        Debug.Log("Brakes: " + engage);
        braking = engage;
    }

    void Update()
    {
        for (int i = 0; i < wheel.Length; i++)
        {
            if (braking)
            {
                wheel[i].motorTorque = 0;
                wheel[i].brakeTorque = brakeTorque;
            }
            else
            {
                wheel[i].motorTorque = 0.0001f;
                wheel[i].brakeTorque = 0;
            }
        }
    }

    ContactPoint[] contacts = new ContactPoint[16];

    private void OnCollisionEnter(Collision collision)
    {
        float impulse = collision.impulse.magnitude / Time.fixedDeltaTime;
        collision.GetContacts(contacts);

        totalDamage += impulse * damageImpulseMult;

        for (int i = 0; i < collision.contactCount; i++)
        {
            Debug.DrawRay(contacts[i].point, Vector3.up * impulse * 0.001f, Color.red, 1);
        }
    }
}

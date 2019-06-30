using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towbar : MonoBehaviour
{
    public Rigidbody towbarRb;

    public float range = 1;

    public Connector connected;
    Connector lastConnected;

    ConfigurableJoint joint;


    void Update()
    {
        if (connected)
        {
            WheelCollider wheel = connected.wheel;

            float angle = Vector3.SignedAngle(connected.transform.forward, transform.forward, connected.transform.up);
            wheel.steerAngle = angle;
        }

        Vector3 towbarPoint = transform.position;

        float closest = Mathf.Infinity;
        Connector closestConnector = null;

        if (lastConnected)
        {
            if (lastConnected && Vector3.Distance(lastConnected.transform.position, towbarPoint) > range)
                lastConnected = null;
            else
                return;
        }

        if (!connected)
        {
            for (int i = 0; i < Connector.all.Count; i++)
            {
                float distance = Vector3.Distance(Connector.all[i].transform.position, transform.position);

                if (distance < range && distance < closest)
                {
                    closest = distance;
                    closestConnector = Connector.all[i];
                }
            }
        }

        if (closestConnector && !connected)
        {
            connected = closestConnector;
            Debug.Log("Connected!");

            Connect(closestConnector);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Disconnect();
    }

    void Connect(Connector connector)
    {
        connected.airplane.EngageBrakes(false);

        joint = towbarRb.gameObject.AddComponent<ConfigurableJoint>();

        joint.axis = Vector3.forward;

        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.angularXMotion = ConfigurableJointMotion.Free;
        joint.angularYMotion = ConfigurableJointMotion.Free;
        joint.angularZMotion = ConfigurableJointMotion.Locked;

        joint.connectedBody = connector.planeRb;

        joint.anchor = transform.localPosition;
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = connector.transform.localPosition;
    }

    void Disconnect()
    {
        if (!connected) return;

        connected.airplane.EngageBrakes(true);

        if (joint)
            Destroy(joint);

        lastConnected = connected;
        connected = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

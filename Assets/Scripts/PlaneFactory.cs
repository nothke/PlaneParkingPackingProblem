using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFactory : MonoBehaviour
{
    public GameObject airplanePrefab;

    public float productionTime = 10;

    float lastProductionTime;
    int planesProduced = 0;

    Airplane airplaneInProduction;
    Airplane lastPlaneInProduction;

    public Transform productionPoint;

    public float lastAircraftInRange = 50;

    private void Start()
    {
        StartProduction();
    }

    public void Update()
    {
        bool timeCondition = Time.time > lastProductionTime + productionTime;

        bool producedPlaneIsNotInTheWay = false;

        if (lastPlaneInProduction)
        {
            float lastProducedDistance = Vector3.Distance(transform.position, lastPlaneInProduction.transform.position);

            if (lastProducedDistance > lastAircraftInRange)
            {
                producedPlaneIsNotInTheWay = true;
                airplaneInProduction = null;
                lastPlaneInProduction = null;
            }
        }
        
        if (timeCondition && airplaneInProduction)
        {
            EndProduction();
        }

        if (!airplaneInProduction && producedPlaneIsNotInTheWay)
        {
            lastProductionTime = Time.time;

            StartProduction();
        }

        float normalizedProductionTime = (Time.time - lastProductionTime) / productionTime;

        if (airplaneInProduction)
        {
            airplaneInProduction.transform.position =
                Vector3.Lerp(productionPoint.position, transform.position, normalizedProductionTime);
        }
    }

    void StartProduction()
    {
        planesProduced++;
        Debug.Log("Produced " + planesProduced);

        GameObject go = Instantiate(airplanePrefab, productionPoint.position, transform.rotation);
        airplaneInProduction = go.GetComponent<Airplane>();

        airplaneInProduction.RB.isKinematic = true;
        //airplaneInProduction.RB.constraints = RigidbodyConstraints.FreezeAll;
        airplaneInProduction.inProduction = true;
    }

    void EndProduction()
    {
        airplaneInProduction.transform.position = transform.position;

        airplaneInProduction.RB.isKinematic = false;
        //airplaneInProduction.RB.constraints = RigidbodyConstraints.None;
        airplaneInProduction.RB.velocity = Vector3.zero;
        airplaneInProduction.RB.angularVelocity = Vector3.zero;

        StartCoroutine(KillPhysics(airplaneInProduction));

        airplaneInProduction.inProduction = false;
        lastPlaneInProduction = airplaneInProduction;
        airplaneInProduction = null;
    }

    IEnumerator KillPhysics(Airplane airplane)
    {
        yield return new WaitForFixedUpdate();

        airplane.RB.constraints = RigidbodyConstraints.None;
        airplane.RB.velocity = Vector3.zero;
        airplane.RB.angularVelocity = Vector3.zero;

        airplane.transform.position = transform.position;
        airplane.transform.rotation = transform.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, lastAircraftInRange);
    }
}

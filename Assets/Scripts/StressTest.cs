using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressTest : MonoBehaviour
{
    public GameObject prefab;

    public float separation = 50;
    public int width = 10;
    public int length = 10;

    void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Instantiate(
                    prefab,
                    transform.position + new Vector3(x * separation, 0, y * separation),
                    Quaternion.identity);
            }
        }
    }
}

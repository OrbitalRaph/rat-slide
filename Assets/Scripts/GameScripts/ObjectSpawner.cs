using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script fait apparaître des objets à intervalles réguliers.
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public float destroyTime = 2.0f;

    public void SpawnObject()
    {
        int randomIndex = Random.Range(0, 2);

        if (randomIndex < 1)
        {
            Instantiate(obstaclePrefab, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(coinPrefab, transform.position, transform.rotation);
        }
    }
}

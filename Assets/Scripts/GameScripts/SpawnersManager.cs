using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script fait apparaître des objets à intervalles réguliers.
/// À chaque intervalle, un spawner est choisi au hasard et un objet est créé.
/// </summary>
public class SpawnersManager : MonoBehaviour
{
    public List<ObjectSpawner> spawners;
    public float spawnInterval = 1.0f;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, spawners.Count);
            spawners[randomIndex].SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

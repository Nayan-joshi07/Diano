using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObjects
    {
        public GameObject prefab;
        [Range(0f,1f)]
        public float spawnChance;
    }

    public SpawnableObjects[] objects;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn),Random.RandomRange(minSpawnRate,maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;
        foreach(var obj in objects) {

            if (spawnChance < obj.spawnChance) {
                GameObject obstacles = Instantiate(obj.prefab);
                obstacles.transform.position += transform.position;
                break;
            }
            spawnChance -= obj.spawnChance;
        }
        Invoke(nameof(Spawn),Random.RandomRange(minSpawnRate, maxSpawnRate));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pravi enemyje dok traje borba sa kraljicom
/// </summary>
public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;                     // enemy gameobject to be spawned
    public float spawnDelay;                    // the difference in time between the two fish

    bool canSpawn;                              // confirms that the fish are spawned after some time delay

    // Use this for initialization
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine("SpawnEnemy");
        }
    }

    IEnumerator SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);     // spawns the jumping fish
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}

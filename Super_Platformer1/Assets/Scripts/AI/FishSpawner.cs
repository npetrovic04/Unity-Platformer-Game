using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// prikazuje skacuce ribe, svakih n sekunde
/// </summary>
public class FishSpawner : MonoBehaviour
{

    public GameObject fish;                     // jumping fish gameobject to be spawned
    public float spawnDelay;                    // the difference in time between the two fish

    bool canSpawn;                              // confirms that the fish are spawned after some time delay

    void Start ()
    {
        canSpawn = true;
    }
	
	
	void Update ()
    {
        if (canSpawn)
        {
            StartCoroutine("SpawnFish");
        }
    }

    IEnumerator SpawnFish()
    {
        Instantiate(fish, transform.position, Quaternion.identity);     // spawns the jumping fish
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}

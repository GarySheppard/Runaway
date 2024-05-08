using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class PrefabSpawner : MonoBehaviour
{
    private Collider prefabCollider;
    private int maxNearbyObjects = 6;
    private Collider[] nearbyObjects;

    public GameObject player;
    public GameObject[] enemyBank;

    void Awake()
    {
        foreach (GameObject enemy in enemyBank)
        {
            ZombieController zombieController = enemy.GetComponent<ZombieController>();
            zombieController.player = player.transform;
        }
    }

    void Start()
    {
        SpawnEnemiesEverywhere(7);
    }

    /* Spawns a specified object at a specified location and rotation */
    public void SpawnObject(GameObject prefab, Vector3 location, Quaternion rotation)
    {
        Vector3 newLocation = location;

        /* Ensures that the location is within the NavMesh */
        NavMeshHit hit;
        NavMesh.SamplePosition(newLocation, out hit, Mathf.Infinity, 1 << 0);
        newLocation = new Vector3(hit.position.x, newLocation.y, hit.position.z);

        Instantiate(prefab, newLocation, rotation);
    }

    /* Spawns a specified object at a specified location and random rotation */
    public void SpawnObject(GameObject prefab, Vector3 location)
    {
        prefabCollider = prefab.GetComponent<Collider>();
        if (prefabCollider == null)
        {
            Debug.Log("The prefab cannot be spawned without a collider");
            return;
        }

        Vector3 newLocation = location;

        /* Gets the number of interactables (physics layer 7) that collide with the prefab at the location */
        nearbyObjects = new Collider[maxNearbyObjects];
        int numNearbyObjects = Physics.OverlapBoxNonAlloc(newLocation, prefabCollider.bounds.size / 2, nearbyObjects, Quaternion.identity, 1 << 7);

        /* Pushes the location until its no longer in contact with interactables */
        /* DOESN'T WORK AS INTENDED, NEEDS FIXING;  */
        /*
        for (int i = 0; i < numNearbyObjects; i++)
        {
            if (nearbyObjects[i] == null)
            {
                continue;
            }
            
            Collider objectCollider = nearbyObjects[i];
            Vector3 objectPosition = objectCollider.gameObject.transform.position;

            Vector3 displaceDirection;
            float displaceDistance;

            bool penetrating = Physics.ComputePenetration(prefabCollider, newLocation, Quaternion.identity, objectCollider, objectPosition, Quaternion.identity, out displaceDirection, out displaceDistance);
            if (penetrating)
            {
                newLocation += (new Vector3(displaceDirection.x, 0, displaceDirection.z) * (displaceDistance + 5.0f));
            }
            //Debug.Log(displaceDirection + " + " + displaceDistance);
            Debug.Log(penetrating);
        }
        */

        /* Ensures that the location is within the NavMesh */
        NavMeshHit hit;
        NavMesh.SamplePosition(newLocation, out hit, Mathf.Infinity, 1 << 0);
        newLocation = new Vector3(hit.position.x, newLocation.y, hit.position.z);

        Quaternion newRotation = new Quaternion(0f, Random.Range(-1f, 1f), 0f, 1f);
        
        SpawnObject(prefab, newLocation, newRotation);
    }

    /* Spawns a specified object at a random location on the NavMesh with a specified y-axis position */
    public void SpawnObject(GameObject prefab, float yPos)
    {
        Vector3 randomLocation = Level.GetRandomLocation(yPos);
        SpawnObject(prefab, randomLocation);
    }

    /* Spawns a specified zombie at a specified location */
    /* [enemyIndex]
     * 0 = Regular Zombie
     * 1 = Grunt Zombie
     * 2 = Brute Zombie
     * 3 = Scout Zombie
     */
    public void SpawnEnemy(int enemyIndex, Vector3 location)
    {
        if (enemyBank.Length <= 0)
        {
            Debug.Log("SpawnEnemy cannot be called since there are no enemies assigned");
        }
        else
        {
            GameObject enemy = enemyBank[enemyIndex];
            SpawnObject(enemy, location);
        }
    }

    /* Spawns random assortment of zombies around a specified location with a specified amount */
    public void SpawnEnemiesNearby(Vector3 location, float range, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomLocation = Level.GetRandomNearbyLocation(location, range);
            randomLocation = new Vector3(randomLocation.x, randomLocation.y - 3.0f, randomLocation.z);
            SpawnEnemy(Random.Range(0, enemyBank.Length), randomLocation);
        }
    }

    /* Spawns random assortment of zombies around a the level with a specified amount */
    public void SpawnEnemiesEverywhere(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomLocation = Level.GetRandomLocation(0.0f);
            SpawnEnemy(Random.Range(0, enemyBank.Length), randomLocation);
        }
    }
}

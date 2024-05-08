using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Level
{
    /* Returns random location on NavMesh with a specified y-axis position */
    public static Vector3 GetRandomLocation(float yPos)
    {
        Vector3 randomLocation = new Vector3(32.4f, -21.3f, -44.4f) + Random.insideUnitSphere * 60.0f;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomLocation, out hit, Mathf.Infinity, 1 << 0);
        return new Vector3(hit.position.x, yPos, hit.position.z);
    }

    /* Returns random location on NavMesh nearby a specified position (with a specified range) */
    public static Vector3 GetRandomNearbyLocation(Vector3 position, float range)
    {
        Vector3 randomLocation = position + Random.insideUnitSphere * range;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomLocation, out hit, Mathf.Infinity, 1 << 0);
        return new Vector3(hit.position.x, position.y, hit.position.z);
    }
}

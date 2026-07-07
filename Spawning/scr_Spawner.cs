using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Spawner : MonoBehaviour
{
    public GameObject spawn, objectSpawned;
    public Vector3 spawnPos;

    public bool isGeorgiaEnemy, isGeorgiaTeleportingEnemy;
    public GameObject obj_pointA, obj_pointB;
    public GameObject[] teleportPoints;

    public void Spawn()
    {
        // sets enemy to spawn and spawn position
        spawnPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        objectSpawned = Instantiate(spawn, spawnPos, Quaternion.identity);

        // spawn other coder default enemy
        if (isGeorgiaEnemy == true)
        {
            objectSpawned.GetComponent<scr_Enemy>().patrolPointA = obj_pointA;
            objectSpawned.GetComponent<scr_Enemy>().patrolPointB = obj_pointB;
        }

        // spawn other coder teleporting enemy
        if (isGeorgiaTeleportingEnemy == true)
        {
            for (int i = 0; i < objectSpawned.GetComponent<scr_Teleport>().teleportPointsGameObjectsArray.Length; i++)
            {
                objectSpawned.GetComponent<scr_Teleport>().teleportPointsGameObjectsArray[i] = teleportPoints[i];
            }
        }
    }

    public void GetRidOff()
    {
        // destroys the enemy spawned
        Destroy(objectSpawned);
    }
}

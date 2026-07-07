using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_LevelSpawnSystem : MonoBehaviour
{
    public GameObject[] spawners;

    public void LevelStart()
    {
        // at beginning of level, spawns all the enemies
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].GetComponent<scr_Spawner>().Spawn();
        }
    }

    public void EndLevel()
    {
        // at end of level, destroys all enemies
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].GetComponent<scr_Spawner>().GetRidOff();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ItemSpawnerManager : MonoBehaviour
{
    public static scr_ItemSpawnerManager instance;

    public GameObject speedUp, damageUp, jumpUp, dashSpeedUp, heal, maxHealthUp, rage, rotatingItem, reviveItem;
    public float randomItemSpawn;

    public static scr_Chest chestSpawner;

    public Vector3 spawnItemPos;

    public void Awake()
    {
        instance = this;
    }

    public void SpawnItem()
    {
        // selects a random item
        randomItemSpawn = Random.Range(0, 9);

        // if rage item is in use and selected, select new random item
        if (randomItemSpawn == 8 && scr_PlayerCombat.instance.rageItemInUse == true)
        {
            randomItemSpawn = Random.Range(0, 8);
        }

        // spawns the selected random item
        switch (randomItemSpawn)
        {
            case 0:
                Instantiate(speedUp, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
            case 1:
                Instantiate(damageUp, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
            case 2:
                Instantiate(jumpUp, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
            case 3:
                Instantiate(dashSpeedUp, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
            case 4:
                Instantiate(heal, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
            case 5:
                Instantiate(maxHealthUp, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
            case 6:
                Instantiate(rotatingItem, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
            case 7:
                Instantiate(reviveItem, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
            case 8:
                Instantiate(rage, new Vector3(spawnItemPos.x, spawnItemPos.y, spawnItemPos.z), Quaternion.identity);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Chase : MonoBehaviour
{
    public GameObject enemy;

    // if player enters trigger area of enemy then chase bool is turned on
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemy.GetComponent<scr_BossEnemy>() != null)
            {
                enemy.GetComponent<scr_BossEnemy>().inChaseRange = true;
            }
            else if (enemy.GetComponent<scr_UnArmouredSkeleton>() != null)
            {
                enemy.GetComponent<scr_UnArmouredSkeleton>().inChaseRange = true;
            }
            else if (enemy.GetComponent<scr_ArmouredSkeleton>() != null)
            {
                enemy.GetComponent<scr_ArmouredSkeleton>().inChaseRange = true;
            }
        }
    }

    // if player exits trigger area of enemy then chase bool is turned off
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerNotSpotted");
            if (enemy.GetComponent<scr_BossEnemy>() != null)
            {
                enemy.GetComponent<scr_BossEnemy>().inChaseRange = false;
            }
            else if (enemy.GetComponent<scr_UnArmouredSkeleton>() != null)
            {
                enemy.GetComponent<scr_UnArmouredSkeleton>().inChaseRange = false;
            }
            else if (enemy.GetComponent<scr_ArmouredSkeleton>() != null)
            {
                enemy.GetComponent<scr_ArmouredSkeleton>().inChaseRange = false ;
            }
        }
    }
}

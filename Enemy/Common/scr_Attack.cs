using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Attack : MonoBehaviour
{
    public GameObject enemy;

    // if player enters trigger area of enemy then attack bool is turned on
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Chase");
            if (enemy.GetComponent<scr_BossEnemy>() != null)
            {
                enemy.GetComponent<scr_BossEnemy>().inAttackRange = true;
            }
            else if (enemy.GetComponent<scr_UnArmouredSkeleton>() != null)
            {
                enemy.GetComponent<scr_UnArmouredSkeleton>().inAttackRange = true;
            }
            else if (enemy.GetComponent<scr_ArmouredSkeleton>() != null)
            {
                enemy.GetComponent<scr_ArmouredSkeleton>().inAttackRange = true;
            }
        }
    }

    // if player exits trigger area of enemy then attack bool is turned off
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerNotSpotted");
            if (enemy.GetComponent<scr_BossEnemy>() != null)
            {
                enemy.GetComponent<scr_BossEnemy>().inAttackRange = false;
            }
            else if (enemy.GetComponent<scr_UnArmouredSkeleton>() != null)
            {
                enemy.GetComponent<scr_UnArmouredSkeleton>().inAttackRange = false;
            }
            else if (enemy.GetComponent<scr_ArmouredSkeleton>() != null)
            {
                enemy.GetComponent<scr_ArmouredSkeleton>().inAttackRange = false;
            }
        }
    }
}

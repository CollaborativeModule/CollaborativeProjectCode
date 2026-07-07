using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerRunOffScreen : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        // if the player enters, call end of level function
        if (other.CompareTag("Player"))
        {
            if (scr_GameManager.instance.numOfLevelsInWorldPlayed < scr_GameManager.instance.numOfLevelsCanPlayInWorld)
            {
                scr_PlayerMovement.instance.EndOfLevel();
            }
            else if (scr_GameManager.instance.numOfLevelsInWorldPlayed >= scr_GameManager.instance.numOfLevelsCanPlayInWorld && scr_PlayerMovement.instance.canEndBossLevel == true)
            {
                scr_PlayerMovement.instance.EndOfBossLevel();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SlamAttackCheck : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        // if the player is hit and damageable, damages the player
        if (other.CompareTag("Player") && scr_SlamAttack.instance.isPlayerDamageable == true)
        {
            scr_SlamAttack.instance.Hit();
        }

        // if hits the ground and can damage the player, attack misses
        else if (other.CompareTag("Ground") && scr_SlamAttack.instance.isPlayerDamageable == true)
        {
            scr_SlamAttack.instance.Miss();
        }
    }
}

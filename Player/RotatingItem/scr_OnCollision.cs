using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_OnCollision : MonoBehaviour
{
    public float damage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // damages collided enemy and plays sound
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<scr_Enemy>().Damage(damage);
            scr_AudioManager.instance.player_Attack.setParameterByName("isOrbHit", 1f);
            scr_AudioManager.instance.player_Attack.setParameterByName("isFireAttack", 0f);
            scr_AudioManager.instance.player_Attack.start();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public Transform tf;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // damages the enemy
        currentHealth -= damage;

        // if enemy is dead, spawn random item and destroy's the enemy
        if (currentHealth <= 0)
        {
            float spawnItem = Random.Range(0, 100);
            spawnItem = 15;
            if (spawnItem <= 20)
            {
                scr_ItemSpawnerManager.instance.spawnItemPos = new Vector3(tf.position.x, tf.position.y, tf.position.z);
                scr_ItemSpawnerManager.instance.SpawnItem();
            }
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}

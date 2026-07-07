using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyDamaged : MonoBehaviour
{
    public GameObject enemy;
    public float maxHealth, health;
    public SpriteRenderer sp;
    public Animator animator;

    public FMODUnity.StudioEventEmitter enemyDamagedEvent;
    public float damageEventParameterNum, deadEventParamterNum;
    public void Damage(float totalDamage)
    {
        //shakes the camera
        print("enemyDamaged");
        scr_CineMachineShake.Instance.ShakeCamera(5f, 0.1f);

        // reduces damage done, if enemy is guarding
        if (enemy.GetComponent<scr_Guard>() != null)
        {
            if (enemy.GetComponent<scr_Guard>().isGuarding == true)
            {
                totalDamage = totalDamage / 10f;
            }
        }

        // deals damage to enemy
        health -= totalDamage;

        // enemy death
        if (health <= 0f)
        {
            animator.SetTrigger("Dead");
            health = 0f;

            // plays selected event and the enemy death track in it
            enemyDamagedEvent.Play();
            enemyDamagedEvent.SetParameter("Enemies_State", deadEventParamterNum);

            // if the enemy has a HP bar, then set the HP bar value
            if (enemy.GetComponent<scr_EnemyHealthBar>() != null)
            {
                enemy.GetComponent<scr_EnemyHealthBar>().SetHealth(maxHealth, health);
            }

            // enemy death function
            Death();
        }

        // enemy damage
        else if (health > 0)
        {
            // plays the damaged animation
            animator.SetTrigger("Damaged");

            // changes enemy sprite colour to be red and after some time invokes the event to turn back to normal
            sp.color = new Vector4 (1, 0, 0, 1);
            Invoke("ColourBackToNormal", 0.2f);

            // if the enemy has a healthbar then set the health bar value's
            if (enemy.GetComponent<scr_EnemyHealthBar>() != null)
            {
                enemy.GetComponent<scr_EnemyHealthBar>().SetHealth(maxHealth, health);
            }

            // plays selected event and the enemy damaged track in it
            enemyDamagedEvent.Play();
            enemyDamagedEvent.SetParameter("Enemies_State", damageEventParameterNum);

        }

        // if attack does no damage
        else if (totalDamage == 0)
        {
            // set HP bar value's if it exists for this enemy
            if (enemy.GetComponent<scr_EnemyHealthBar>() != null)
            {
                enemy.GetComponent<scr_EnemyHealthBar>().currentHealth = maxHealth;
                enemy.GetComponent<scr_EnemyHealthBar>().SetHealth(maxHealth, health);
            }
        }
    }

    public void Death()
    {
        // if the enemy is a boss, then the enemy killed event is played
        if (enemy.GetComponent<scr_BossEnemy>() != null)
        {
            enemy.GetComponent<scr_BossEnemy>().Killed();
        }
        else if(enemy.GetComponent<script_SunRa>() != null)
        {

        }
        // if any other enemy, random chance of spawning item and the enemy is deleted
        else
        {
            float randomSpawnItemChance = Random.Range(0, 100);
            if (randomSpawnItemChance <= 20)
            {
                scr_ItemSpawnerManager.instance.spawnItemPos = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
                scr_ItemSpawnerManager.instance.SpawnItem();
            }
            Destroy(gameObject);
        }
    }

    public void ColourBackToNormal()
    {
        // changes enemy sprite colour back to normal
        sp.color = new Vector4 (255, 255, 255, 255); 
    }
}

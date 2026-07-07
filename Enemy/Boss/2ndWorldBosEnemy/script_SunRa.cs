using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class script_SunRa : MonoBehaviour
{
    public float maxHealth = 2000f;
    public float health = 2000f;
    public float baseDamage= 10f;
    public float attackDelay = 0.5f;
    public float moveSpeed = 1f;

    public Animator animator;
    public bool in2ndPhase, isAttacking;
    public Transform playerTransform, enemyTransform;

    public GameObject enemy, player, leftHand, rightHand, mainBody, itemReward;
    public SpriteRenderer sp, mainBodySprite, leftArmSprite, rightArmSprite;

    public static script_SunRa instance;
    public void Awake()
    {
        // sets enemy values
        instance = this;
        enemy.GetComponent<scr_EnemyDamaged>().health = health;
        enemy.GetComponent<scr_EnemyDamaged>().maxHealth = maxHealth;
    }

    public void Update()
    {
        // if the player value is null, then set the player value's and enemy HP bar
        if (player == null)
        {
            player = scr_GameManager.instance.player;
            playerTransform = player.GetComponent<Transform>();
            enemy.GetComponent<scr_EnemyHealthBar>().bossEnemySetHealthBar();
            enemy.GetComponent<scr_EnemyHealthBar>().SetHealth(maxHealth, health);
        }

        // sets the boss to the second phase, when low enemy HP
        if (health <= 1000 && health > 0)
        {
            in2ndPhase = true;
            if (leftHand.layer == 7)
            {
                leftHand.layer = 0;
                rightHand.layer = 0;
                mainBody.layer = 7;
                leftHand.GetComponent<scr_SunRaHands>().EndFirstPhase();
                rightHand.GetComponent<scr_SunRaHands>().EndFirstPhase();
                scr_AudioManager.instance.world_2_Boss_Enemy.setParameterByName("Boss_World_2_State", 2f);
                scr_AudioManager.instance.world_2_Boss_Enemy.start();
            }
        
        // sets boss to the first phase value's
        }
        else if (health > 1000)
        {
            in2ndPhase = false;
            if (leftHand.layer != 7)
            {
                leftHand.layer = 7;
                rightHand.layer = 7;
                mainBody.layer = 0;
            }
        }

        // calls enemy death
        else if (health <= 0)
        {
            Death();
        }

        // starts first phase enemy attack, if not already attacking and in the first phase
        if (in2ndPhase == false && isAttacking == false)
        {
            isAttacking = true;
            FirstPhaseAttacks();
        }

        // starts second phase enemy attack, if not already attacking and in the second phase
        else if (in2ndPhase == true && isAttacking == false)
        {
            isAttacking = true;
            SecondPhaseAttacks();
        }
    }

    public void FirstPhaseAttacks()
    {
        // select enemy attack patern
        float attackType = Random.Range(0,4);

        // calls enemy attacks in the pattern
        switch (attackType)
        {
            case 0:
            Invoke("SlamAttack",0f);
            Invoke("SlamAttack",8.5f);
            Invoke("SlamAttack",17f);
            Invoke("WaveAttack",34f);
            Invoke("Exhausted",39f);
            break;
            case 1:            
            Invoke("SlamAttack",0f);
            Invoke("SlamAttack",8.5f);
            Invoke("Exhausted",17f);
            break;
            case 2:
            Invoke("WaveAttack",0f);
            Invoke("WaveAttack",5f);
            Invoke("WaveAttack",10f);
            Invoke("Exhausted",15f);
            break;
            case 3:
            Invoke("SlamAttack",0f);
            Invoke("SlamAttack",8.5f);
            Invoke("WaveAttack",17f);
            Invoke("SlamAttack",22f);
            Invoke("Exhausted",30.5f);
            break;
        }
    }

    public void SecondPhaseAttacks()
    {
        // select enemy attack patern
        float attackType = Random.Range(0,4);

        // calls enemy attacks in the pattern
        switch (attackType)
        {
            case 0:
            Invoke("WaveAttack",0f);
            Invoke("MeteorAttack",5f);
            Invoke("SlamAttack",9f);
            Invoke("Exhausted",17.5f);
            break;
            case 1:            
            Invoke("SlamAttack",0f);
            Invoke("MeteorAttack",8.5f);
            Invoke("SlamAttack",12.5f);
            Invoke("Exhausted",21f);
            break;
            case 2:
            Invoke("MeteorAttack",0f);
            Invoke("WaveAttack",4f);
            Invoke("WaveAttack",9f);
            Invoke("MeteorAttack",14f);
            Invoke("Exhausted",18f);
            break;
            case 3: // 2x fist, wave, fist, rest
            Invoke("MeteorAttack",0f);
            Invoke("SlamAttack",4f);
            Invoke("Exhausted",12.5f);
            break;
        }
    }

    public void WaveAttack()
    {
        // randomises which way the enemy attack comes from
        float randomHand = Random.Range(0,2);
        if (randomHand == 0)
        {
            leftHand.GetComponent<scr_WaveAttack>().WaveAttack();
        }
        else if (randomHand == 1)
        {
            rightHand.GetComponent<scr_WaveAttack>().WaveAttack();
        }

        // plays enemy attack sound
        scr_AudioManager.instance.world_2_Boss_Enemy.setParameterByName("Boss_World_2_State", 4f);
        scr_AudioManager.instance.world_2_Boss_Enemy.start();
    }

    public void SlamAttack()
    {
        // randomises which way the enemy attack comes from
        float randomHand = Random.Range(0,2);
        if (randomHand == 0)
        {
            leftHand.GetComponent<scr_FireSlam>().OnAttack();
        }
        else if (randomHand == 1)
        {
            rightHand.GetComponent<scr_FireSlam>().OnAttack();
        }

        // plays enemy attack sound
        scr_AudioManager.instance.world_2_Boss_Enemy.setParameterByName("Boss_World_2_State", 5f);
        scr_AudioManager.instance.world_2_Boss_Enemy.start();
    }

    public void MeteorAttack()
    {
        // spawns meteors and plays enemy attack sound
        enemy.GetComponent<scr_Meteor>().SpawnAttack();
        scr_AudioManager.instance.world_2_Boss_Enemy.setParameterByName("Boss_World_2_State", 3f);
        scr_AudioManager.instance.world_2_Boss_Enemy.start();
    }

    public void Exhausted()
    {
        // sets enemy animation and invokes end of exhaustion
        animator.SetTrigger("Exhausted");
        Invoke("ExhaustedDelay", 3f);
    }

    public void ExhaustedDelay()
    {
        // makes the enemy no longer exhausted and allows it to attack again
        isAttacking = false;
        animator.SetTrigger("Normal");
    }

    public void Death()
    {
        // sets the end level values, drops item and destroy's boss
        scr_GameManager.instance.bossHealthBar.SetActive(false);
        scr_PlayerMovement.instance.canEndBossLevel = true;
        Instantiate(itemReward, new Vector3 (transform.position.x, transform.position.y -2f, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        // changes enemy sprite to damaged color
        enemy.GetComponent<scr_EnemyDamaged>().Damage(damage);
        mainBodySprite.color = new Vector4(1, 0, 0, 1);
        leftArmSprite.color = new Vector4(1, 0, 0, 1);
        rightArmSprite.color = new Vector4(1, 0, 0, 1);
        Invoke("ColourBackToNormal", 0.2f);
    }
    public void ColourBackToNormal()
    {
        // changes enemy sprite colour back to normal
        mainBodySprite.color = new Vector4(255, 255, 255, 255);
        leftArmSprite.color = new Vector4(255, 255, 255, 255);
        rightArmSprite.color = new Vector4(255, 255, 255, 255);
    }
}

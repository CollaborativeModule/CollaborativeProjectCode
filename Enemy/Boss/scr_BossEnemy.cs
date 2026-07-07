using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BossEnemy : MonoBehaviour, scr_IBaseEnemy
{
    public float maxHealth // getting and setting the maxHealth variable from the interface scr_IBaseEnemy
    {
        get
        {
           return 1000f;
        }
        set
        {
            maxHealth = 1000f;
        }
    }

    public float health  // getting and setting the HP variable
    {
        get
        {
            return 1000f;
        }
        set
        {
            health = 1000f;
        }
    }

    public float baseDamage // getting and setting the base damage variable
    {
        get
        {
            return 10f;
        }
        set
        {
            baseDamage = 10f;
        }
    }
    public float attackDelay // getting and setting the attack delay variable
    {
        get
        {
            return 0.5f;
        }
        set
        {
            attackDelay = 0.5f;
        }
    }

    public float moveSpeed // getting and setting the move speed variable
    {
        get
        {
            return 1f;
        }
        set
        {
            moveSpeed = 1f;
        }
    }

    public GameObject enemy, itemReward, player;
    public Rigidbody2D rb;
    public Transform enemyTf, playerTf, chaseDistanceTf, attackDistanceTf;
    public SpriteRenderer sp;

    public bool inChaseRange, inAttackRange, hasAttacked, isExhausted;

    public static scr_BossEnemy instance;

    public Animator animator;

    public float idleSoundDelay, walkSoundDelay;
    private bool playedIdleSound, walkSoundPlayed;

    public void Awake()
    {
        // sets enemy values
        instance = this;
        enemy.GetComponent<scr_EnemyDamaged>().health = maxHealth;
        enemy.GetComponent<scr_EnemyDamaged>().sp = sp;
    }

    public void Idle()
    {
        // sets enemy not to move
        rb.velocity = new Vector2(0f, 0f);
    }

    public void Update()
    {
        // if player value is null, sets player value
        if(playerTf == null)
        {
            playerTf = scr_GameManager.instance.player.GetComponent<Transform>();
        }

        // if the player is in chase range but not attack range and isn't exhaused, then chase the player
        if (inChaseRange == true && inAttackRange == false && isExhausted == false)
        {
            Chase();
            animator.SetTrigger("Walking");

            if (walkSoundPlayed == false)
            {
                walkSoundPlayed = true;
                scr_AudioManager.instance.world_1_Boss_Enemy.setParameterByName("Boss_World_1_State", 1f);
                scr_AudioManager.instance.world_1_Boss_Enemy.start();
            }
            else
            {
                walkSoundDelay -= Time.deltaTime;
                if (walkSoundDelay <= 0)
                {
                    walkSoundDelay = 0;
                    walkSoundPlayed = false;
                }
            }
        }

        // if the player is in the attack range  and isn't exhausted then do the attack event
        else if (inAttackRange == true && hasAttacked == false && isExhausted == false)
        {
            hasAttacked = true;
            Invoke("Attack", 0.2f);
        }
        // if the player is not in chase or attack range  and isn't exhausted then be idle
        else if (inChaseRange == false && inAttackRange == false && isExhausted == false)
        {
            Idle();
            animator.SetTrigger("Idle");

            if (playedIdleSound == false)
            {
                playedIdleSound = true;
                scr_AudioManager.instance.world_1_Boss_Enemy.setParameterByName("Boss_World_1_State", 0f);
                scr_AudioManager.instance.world_1_Boss_Enemy.start();
            }
            else
            {
                idleSoundDelay -= Time.deltaTime;
                if (idleSoundDelay <= 0)
                {
                    idleSoundDelay = 0;
                    playedIdleSound = false;
                }
            }
        }

        // sets the enemy attack range
        enemy.GetComponent<scr_SpinAttack>().inAttackRange = inAttackRange;
    }

    public void Exhausted() // stops enemy movement and makes them unable to damage the enemy for a set amount of time
    {
        isExhausted = true;
        rb.velocity = new Vector2(0f, 0f);
        animator.SetTrigger("Exhausted");
        Invoke("ExhaustedDelay", 5f);
    }

    public void ExhaustedDelay() // makes the enemy no longer exhausted and allows it to attack again
    {
        isExhausted = false;
        hasAttacked = false;
    }

    public void Chase()
    {
        // moves the AI towards the player position and then calculates the distance from the AI to the player
        transform.position = Vector2.Lerp(transform.position, playerTf.position, moveSpeed * Time.deltaTime);
        float playerSidePos = transform.position.x - playerTf.position.x;

        // if the enemy is to the left hand side of the player then check if the sprite is already facing that direction and if not rotate some transforms and then flip the sprite.
        if (playerSidePos < 0)
        {
            if (sp.flipX != true)
            {
                chaseDistanceTf.Rotate(chaseDistanceTf.rotation.x, 180f, chaseDistanceTf.rotation.z, Space.World);
                attackDistanceTf.Rotate(attackDistanceTf.rotation.x, 180f, attackDistanceTf.rotation.z, Space.World);
            }
            sp.flipX = true;
        }
        // if the enemy is to the right hand side of the player then check if the sprite is already facing that direction and if not rotate some transforms and then flip the sprite.
        else if (playerSidePos > 0)
        {
            if (sp.flipX != false)
            {
                chaseDistanceTf.Rotate(chaseDistanceTf.rotation.x, 180f, chaseDistanceTf.rotation.z, Space.World);
                attackDistanceTf.Rotate(attackDistanceTf.rotation.x, 180f, attackDistanceTf.rotation.z, Space.World);
            }
            sp.flipX = false;
        }
    }

    public void Attack() // gets a value between 0 and 5 then does the associated attack combo and animations based on it and then triggers the exhausted function at the end of the combo of attaks
    {
        float attackCombo = Random.Range(1, 2);
        switch (attackCombo)
        {
            case 0:
                enemy.GetComponent<scr_BasicMeleeAttack>().Melee(baseDamage, inAttackRange);
                animator.SetTrigger("MeleeAttack");
                scr_AudioManager.instance.world_1_Boss_Enemy.setParameterByName("Boss_World_1_State", 6f);
                scr_AudioManager.instance.world_1_Boss_Enemy.start();
                Invoke("Exhausted", 0.5f);
                break;
            case 1:
                enemy.GetComponent<scr_SpinAttack>().SpinAttackEnabled(baseDamage, 3f);
                animator.SetTrigger("SwingAttack");
                scr_AudioManager.instance.world_1_Boss_Enemy.setParameterByName("Boss_World_1_State", 4f);
                scr_AudioManager.instance.world_1_Boss_Enemy.start();
                Invoke("Exhausted", 3f);
                break;
            case 2:
                float playerSidePos = transform.position.x - playerTf.position.x;
                enemy.GetComponent<scr_SlamAttack>().Melee(baseDamage, inAttackRange, rb, playerSidePos);
                scr_AudioManager.instance.world_1_Boss_Enemy.setParameterByName("Boss_World_1_State", 5f);
                scr_AudioManager.instance.world_1_Boss_Enemy.start();
                animator.SetTrigger("SlamAttack");
                Invoke("Exhausted", 2f);
                break;
            case 3:
                Invoke("NormalDelay", 0f);
                Invoke("NormalDelay", 0.5f);
                Invoke("NormalDelay", 1f);
                Invoke("Exhausted", 1.5f);
                break;
            case 4:
                Invoke("SlamDelay", 0f);
                Invoke("SlamDelay", 2.5f);
                Invoke("SlamDelay", 5f);
                Invoke("Exhausted", 7.25f);
                break;
            case 5:
                Invoke("NormalDelay", 0f);
                Invoke("SlamDelay", 1.25f);
                Invoke("Exhausted", 4f);
                break;
        }
    }

    public void SlamDelay() // gets the player pos and feeds that and other info into the slam attack function
    {
        float playerSidePos = transform.position.x - playerTf.position.x;
        enemy.GetComponent<scr_SlamAttack>().Melee(baseDamage, inAttackRange, rb, playerSidePos);
        animator.SetTrigger("SlamAttack");
        scr_AudioManager.instance.world_1_Boss_Enemy.setParameterByName("Boss_World_1_State", 5f);
        scr_AudioManager.instance.world_1_Boss_Enemy.start();
    }

    public void NormalDelay() // triggers and feeds info into the basic melee attack function
    {
        enemy.GetComponent<scr_BasicMeleeAttack>().Melee(baseDamage, inAttackRange);
        animator.SetTrigger("MeleeAttack");
        scr_AudioManager.instance.world_1_Boss_Enemy.setParameterByName("Boss_World_1_State", 6f);
        scr_AudioManager.instance.world_1_Boss_Enemy.start();
    }

    public void Killed()
    {
        // destroy's the enemy and drops an item
        scr_GameManager.instance.bossHealthBar.SetActive(false);
        scr_PlayerMovement.instance.canEndBossLevel = true;
        Instantiate(itemReward, new Vector3 (enemyTf.position.x, enemyTf.position.y + 0.5f, enemyTf.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}

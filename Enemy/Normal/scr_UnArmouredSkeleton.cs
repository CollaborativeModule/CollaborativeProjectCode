using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_UnArmouredSkeleton : MonoBehaviour, scr_IBaseEnemy
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

    public float health
    {
        get
        {
            return 100f;
        }
        set
        {
            health = 100f;
        }
    }

    public float baseDamage
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
    public float attackDelay
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

    public float moveSpeed
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

    public GameObject enemy;
    public Rigidbody2D rb;
    public Transform enemyTf, playerTf, chaseDistanceTf, attackDistanceTf;
    public SpriteRenderer sp;
    public bool inChaseRange, inAttackRange, hasAttacked, isExhausted;

    public Animator animator;

    public float idleSoundDelay, walkSoundDelay, baseWalkSoundDelay, baseIdleSoundDelay;
    public bool idleSoundPlayed, walkSoundPlayed;

    public void Awake()
    {

    }

    public void Update()
    {
        // if the player is in chase range but not attack range then do the chase event and isn't exhausted
        if (inChaseRange == true && inAttackRange == false && isExhausted == false)
        {
            Chase();
            animator.SetTrigger("Walking");

            // plays enemy walk sound if not played
            if (walkSoundPlayed == false)
            {
                walkSoundPlayed = true;
                scr_AudioManager.instance.skeleton_Enemy.setParameterByName("Enemies_State", 1f);
                scr_AudioManager.instance.skeleton_Enemy.start();
            }

            // enemy walk sound delay
            else
            {
                walkSoundDelay -= Time.deltaTime;
                if (walkSoundDelay <= 0)
                {
                    walkSoundPlayed = false;
                    walkSoundDelay = baseWalkSoundDelay;
                }
            }
        }

        // if the player is in the attack range then do the attack event and isn't exhausted
        else if (inAttackRange == true && hasAttacked == false && isExhausted == false)
        {
            hasAttacked = true;
            Invoke("Attack", 0.2f);
        }

        // if the player is not in chase or attack range then be idle and isn't exhausted
        else if (inChaseRange == false && inAttackRange == false && isExhausted == false)
        {
            Idle();
            animator.SetTrigger("Idle");

            // plays idle sound if not played
            if (idleSoundPlayed == false)
            {
                idleSoundPlayed = true;
                scr_AudioManager.instance.skeleton_Enemy.setParameterByName("Enemies_State", 0f);
                scr_AudioManager.instance.skeleton_Enemy.start();
            }

            // idle sound delay
            else
            {
                idleSoundDelay -= Time.deltaTime;
                if (idleSoundDelay <= 0)
                {
                    idleSoundPlayed = false;
                    idleSoundDelay = baseIdleSoundDelay;
                }
            }
        }
    }

    public void Idle()
    {
        // sets enemy movement to 0,0
        rb.velocity = new Vector2(0f, 0f);
    }

    public void Chase()
    {
        // smoothly moves enemy towards player
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

     public void Attack()
    {
        // triggers basic attack towards the player, then enters exhaused state
        enemy.GetComponent<scr_BasicMeleeAttack>().Melee(baseDamage, inAttackRange);
        scr_AudioManager.instance.skeleton_Enemy.setParameterByName("Enemies_State", 2f);
        scr_AudioManager.instance.skeleton_Enemy.start();
        animator.SetTrigger("MeleeAttack");
        Invoke("Exhausted", 0.5f);
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

}

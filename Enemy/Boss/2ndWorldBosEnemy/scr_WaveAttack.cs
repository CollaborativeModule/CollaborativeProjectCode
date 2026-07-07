using System.Collections;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

public class scr_WaveAttack : MonoBehaviour
{
    public Transform playerTf, enemyTf;
    public Vector3 enemyPos, playerPos;
    public bool isAttacking, hasHit;
    public float baseAttackTime, attackTime, attackMoveSpeed;

    public void Update()
    {
        if (isAttacking == true && hasHit == false) // if the attack is being done and the player hasn't been hit by the attack
        {
            // this moves the attack in the direction the player is located relative to the enemy
            if (playerPos.x >= 0) 
            {
                enemyTf.position = new Vector3(enemyTf.position.x + 1 * attackMoveSpeed * Time.deltaTime, enemyTf.position.y, enemyTf.position.z);
            }
            else if (playerPos.x < 0)
            {
                enemyTf.position = new Vector3(enemyTf.position.x - 1 * attackMoveSpeed * Time.deltaTime, enemyTf.position.y, enemyTf.position.z);
            }

            // this is a timer which once the timer is done it ends the attack 
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                isAttacking = false;
                EndAttack();
            }

        // if attacking and has hit the player, ends the attack
        }
        else if (isAttacking == true && hasHit == true)
        {
            isAttacking = false;
            EndAttack();
        }
    }

    public void EndAttack()
    {
        // sets the attack time back up to what it was before the attack
        attackTime = baseAttackTime;
        // resets the attacking objects position back to what it was at the start of the attack
        enemyTf.position = enemyPos;
    }
    public void WaveAttack()
    {
        //gets the enemy position based on current position of the attacking object
        playerTf = script_SunRa.instance.playerTransform;
        enemyPos = enemyTf.position;
        
        // gets the player position based on where it is from the enemy
        playerPos = playerTf.position - transform.position;

        // sets the bools to allow the enemy to attack
        hasHit = false;
        isAttacking = true;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        // if what enters the trigger box is the player and the attack is active and hasn't damaged the player before in the attack
        if (other.CompareTag("Player") && isAttacking == true && hasHit == false)
        {
            // sets the bool to that the player has been hit, to prevent repeat damage from the attack
            hasHit = true;

            // damages the player based on the damage variable in the main boss script multipled by 2
            scr_PlayerCombat.instance.enemyDamageAmount = script_SunRa.instance.baseDamage * 2;
            scr_PlayerCombat.instance.OnDamaged();
        }
    }
}

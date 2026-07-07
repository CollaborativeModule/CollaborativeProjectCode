using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_FireSlam : MonoBehaviour
{
    public Transform playerTf;
    public float moveSpeed, attackMoveSpeed, playerLaunch;
    public Vector3 attackPos, enemyPos;

    public bool isAttacking, isMovingUp, isMovingTowardsPlayer, isGoingDown;

    public bool isTouchingGround;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float checkRadius;

    public void Update()
    {
        // if enemy is attacking
        if (isAttacking == true)
        {
            // enemy is moving upwards
            if (isMovingUp == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1 * moveSpeed * Time.deltaTime, transform.position.z);

                // once moved upwards, move towards player
                if (transform.position.y >= playerTf.position.y + 6f)
                {
                    isMovingUp = false;
                    isMovingTowardsPlayer = true;
                    attackPos = playerTf.position;
                }
            }

            // moving towards player position
            else if (isMovingTowardsPlayer == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(attackPos.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

                // once at the attack position, move downwards
                if (transform.position.x == attackPos.x)
                {
                    isMovingTowardsPlayer = false;
                    isGoingDown = true;
                }
            }

            // moving donwards
            else if (isGoingDown == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1 * attackMoveSpeed * Time.deltaTime, transform.position.z);

                // check if hit the ground
                isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

                // if hit the ground, enemy attack and return to the enemy body
                if (isTouchingGround == true)
                {
                    isGoingDown = false;
                    isAttacking = false;
                    transform.position = enemyPos;
                }
            }
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        // if hits player and is attacking while slamming, launch player up and damage them
        if (other.CompareTag("Player") && isAttacking == true && isGoingDown == true)
        {
            scr_PlayerMovement.instance.LaunchUp(playerLaunch);
            scr_PlayerCombat.instance.enemyDamageAmount = script_SunRa.instance.baseDamage * 2;
            scr_PlayerCombat.instance.OnDamaged();
        }
    }

    public void OnAttack()
    {
        // sets values for attacking the player
        playerTf = script_SunRa.instance.playerTransform;
        enemyPos = transform.position;
        isAttacking = true;
        isMovingUp = true;
    }
}

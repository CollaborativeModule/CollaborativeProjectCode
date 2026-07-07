using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SlamAttack : MonoBehaviour
{
    public float jumpUpDistance, sideWaysDistance;
    public bool isPlayerDamageable;
    private float enemyDamage;
    public GameObject hitPlayerCollider;

    public static scr_SlamAttack instance;

    public void Awake()
    {
        instance = this;
    }

    public void Melee(float damage, bool inAttackRange, Rigidbody2D rb, float playerPos)
    {
        // launch enemy up
        enemyDamage = damage;
        rb.AddForce(transform.up * jumpUpDistance, ForceMode2D.Impulse);
        if (playerPos > 0)
        {
            rb.AddForce(-transform.right * sideWaysDistance, ForceMode2D.Impulse);
        }
        else if (playerPos < 0)
        {
            rb.AddForce(transform.right * sideWaysDistance, ForceMode2D.Impulse);
        }
        Invoke("CheckIfPlayerHit", 0.25f);
    }

    public void CheckIfPlayerHit()
    {
        // sets values to allow player being damaged
        isPlayerDamageable = true;
        hitPlayerCollider.SetActive(true);
    }

    public void Hit()
    {
        // damages enemy and disallows player being damaged
        isPlayerDamageable = false;
        scr_PlayerCombat.instance.enemyDamageAmount = enemyDamage;
        scr_PlayerCombat.instance.OnDamaged();
        hitPlayerCollider.SetActive(false);
    }
    
    public void Miss()
    {
        // disallows player being damaged
            isPlayerDamageable = false;
            hitPlayerCollider.SetActive(false);
    }

    public void Update()
    {
        // if enemy is exhaused, disallows player being damaged
        if (scr_BossEnemy.instance.isExhausted == true)
        {
            isPlayerDamageable = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SpinAttack : MonoBehaviour
{
    public float baseRotation, rotation, rotationSpeed, baseDamageDelay, damageDelay;
    public bool isAttack, inAttackRange, doneDamage;

    public float damage;

    void Update()
    {
        if (isAttack == true) // rotates the enemy if this attack is done
        {
            transform.localEulerAngles = new Vector3(0, rotation, 0);
            rotation += rotationSpeed * Time.deltaTime;
            scr_BossEnemy.instance.Chase();
            print("enemy spinning");
        }
        else if (isAttack == false) // stops rotating and sets the enemy rotation back to normal once the attack is done
        {
            rotation = baseRotation;
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        if (isAttack == true && inAttackRange == true && doneDamage == false) // if the player is in the attack range, the enemy is doing this attack and the player hasn't been damaged recently, then damage the player.
        {
            doneDamage = true;
            scr_PlayerCombat.instance.enemyDamageAmount = damage;
            scr_PlayerCombat.instance.OnDamaged();
            print("enemy spin attack hit");
        }

        if (doneDamage == true) // if player damaged recently, then do a cooldown on damaging the player
        {
            damageDelay -= Time.deltaTime;
            if (damageDelay <= 0)
            {
                damageDelay = baseDamageDelay;
                doneDamage = false;
            }
        }
    }

    public void SpinAttackEnabled(float damageToDo, float delay) // sets the damage of the attack and allows the attack to begin, also starts a countdown to the end of the attack
    {
        damage = damageToDo;
        isAttack = true;
        Invoke("SpinAttackDisabled", delay);
    }

    public void SpinAttackDisabled() // ends the attack
    {
        isAttack = false;
    }
}

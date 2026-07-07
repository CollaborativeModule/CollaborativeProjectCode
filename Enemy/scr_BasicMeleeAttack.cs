using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BasicMeleeAttack : MonoBehaviour
{
    public void Melee(float damage, bool inAttackRange)
    {
        // damage's the player when in attack range
        if (inAttackRange == true)
        {
            scr_PlayerCombat.instance.enemyDamageAmount = damage;
            scr_PlayerCombat.instance.OnDamaged();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface scr_IBaseEnemy
{
    // base value's for different enemy script's
    float maxHealth {  get; set; }
    float health { get; set; }

    float baseDamage { get; set; }
    float attackDelay { get; set; }

    float moveSpeed { get; set; }

    void Idle();

    void Chase();

    void Attack();

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerCombat : MonoBehaviour
{

    public Animator animtor;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    public float baseDamage, damageUp, totalDamage;

    public float attackCoolDown, baseCoolDown, damageUpCoolDown;
    public bool canAttack, isDamageUp;

    public float playerHealth, maxPlayerHealth, enemyDamageAmount;
    public bool isPlayerRevivable;

    public float rageItemCoolDown, rageItemEnemyAttackUpMultiplayer;
    public float rageItemAttackUpNormalMultiplayer, rageItemAttackUpLowHealthMultiplayer;
    public bool rageItemInUse, isLowHealth;

    public bool isFireWallPickedUp;
    public GameObject fireWall;

    public static scr_PlayerCombat instance;

    public bool hasPlayedRageSound;

    public void Awake()
    {
        // sets value at awake
        instance = this;
        canAttack = true;
        attackCoolDown = baseCoolDown;
    }

    public void GameStart()
    {
        // sets value at game start
        isFireWallPickedUp = false;
        isPlayerRevivable = false;
        rageItemInUse = false;
        isLowHealth = false;
        canAttack = true;
        isDamageUp = false;
        rageItemCoolDown = 0;
        attackCoolDown = baseCoolDown;
        damageUpCoolDown = 0;
        playerHealth = 100;
        maxPlayerHealth = 100;
        enemyDamageAmount = 0;
        rageItemCoolDown = 0;
        damageUp = 0;
        scr_HealthBar.instance.SetHealth(maxPlayerHealth, playerHealth, 0f);
    }

    void Update()
    {
        // player attack if alloweed
        if (Input.GetButtonDown("Fire1") && canAttack == true && scr_GameManager.instance.playerCanDoStuff == true)
        {
            canAttack = false;
            MeleeAttack();

            // if fire wall is picked up, then spawns the firewall
            if (isFireWallPickedUp == true)
            {
                SpawnFireWall();
                scr_AudioManager.instance.player_Attack.setParameterByName("isFireAttack", 1f);
            }

            // plays sound
            scr_AudioManager.instance.player_Attack.setParameterByName("isOrbHit", 0f);
            scr_AudioManager.instance.player_Attack.start();
        }

        // cool down for next attack
        if (canAttack == false)
        {
            attackCoolDown -= Time.deltaTime;

            // reset values to default
            if (attackCoolDown <= 0)
            {
                canAttack = true;
                attackCoolDown = baseCoolDown;
            }
        }

        // timer for how long damage up lasts for
        if (isDamageUp == true)
        {
            scr_GameManager.instance.damageUp.SetActive(true);
            damageUpCoolDown -= Time.deltaTime;
            scr_GameManager.instance.damageUpText.text = "" + Mathf.Round(damageUpCoolDown);

            // resets value to base stats
            if (damageUpCoolDown <= 0)
            {
                damageUp = 0;
                isDamageUp = false;
                damageUpCoolDown = 0;
                scr_GameManager.instance.damageUp.SetActive(false);
            }
        }

        // timer for how long rage lasts for
        if (rageItemInUse == true)
        {
            scr_GameManager.instance.rageItem.SetActive(true);
            rageItemCoolDown -= Time.deltaTime;
            scr_GameManager.instance.rageItemText.text = "" + Mathf.Round(rageItemCoolDown);

            // plays rage sound once
            if (hasPlayedRageSound == false)
            {
                hasPlayedRageSound = true;
                scr_AudioManager.instance.player_Rage.setParameterByName("Player_Rage_Status", 0f);
                scr_AudioManager.instance.player_Rage.start();
            }

            // reset values to default
            if (rageItemCoolDown <= 0)
            {
                scr_GameManager.instance.rageItem.SetActive(false);
                rageItemInUse = false;
                hasPlayedRageSound = true;
                scr_AudioManager.instance.player_Rage.setParameterByName("Player_Rage_Status", 1f);
                scr_AudioManager.instance.player_Rage.start();
            }
        }
    }

    public void MeleeAttack()
    {

        // calculates amount of damage of attack
        calculateDamage();

        // triggers attack animation and detects how many enemies are hit
        animtor.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // damages every enemy hit with some slight differences based on variables
        foreach (Collider2D enemy in hitEnemies)
        {
            if (isLowHealth == true && rageItemInUse == true)
            {
                if (enemy.GetComponent<scr_EnemyDamaged>() != null)
                {
                    enemy.GetComponent<scr_EnemyDamaged>().Damage(totalDamage * rageItemAttackUpLowHealthMultiplayer);
                }
                else if (enemy.GetComponent<script_SunRa>().leftHand.GetComponent<scr_SunRaHands>() != null)
                {
                    enemy.GetComponent<script_SunRa>().leftHand.GetComponent<scr_SunRaHands>().Damaged(totalDamage * rageItemAttackUpLowHealthMultiplayer);
                }
                else if (enemy.GetComponent<script_SunRa>().rightHand.GetComponent<scr_SunRaHands>() != null)
                {
                    enemy.GetComponent<script_SunRa>().rightHand.GetComponent<scr_SunRaHands>().Damaged(totalDamage * rageItemAttackUpLowHealthMultiplayer);
                }
                else if (enemy.GetComponent<script_SunRa>().mainBody.GetComponent<scr_SunRaHands>() != null)
                {
                    enemy.GetComponent<script_SunRa>().mainBody.GetComponent<scr_SunRaHands>().Damaged(totalDamage * rageItemAttackUpLowHealthMultiplayer);
                }
            }
            else if (isLowHealth == false && rageItemInUse == true)
            {
                if (enemy.GetComponent<scr_EnemyDamaged>() != null)
                {
                    enemy.GetComponent<scr_EnemyDamaged>().Damage(totalDamage * rageItemAttackUpNormalMultiplayer);
                }
                else if (enemy.GetComponent<script_SunRa>().leftHand.GetComponent<scr_SunRaHands>() != null)
                {
                    enemy.GetComponent<script_SunRa>().leftHand.GetComponent<scr_SunRaHands>().Damaged(totalDamage * rageItemAttackUpLowHealthMultiplayer);
                }
                else if (enemy.GetComponent<script_SunRa>().rightHand.GetComponent<scr_SunRaHands>() != null)
                {
                    enemy.GetComponent<script_SunRa>().rightHand.GetComponent<scr_SunRaHands>().Damaged(totalDamage * rageItemAttackUpLowHealthMultiplayer);
                }
                else if (enemy.GetComponent<script_SunRa>().mainBody.GetComponent<scr_SunRaHands>() != null)
                {
                    enemy.GetComponent<script_SunRa>().mainBody.GetComponent<scr_SunRaHands>().Damaged(totalDamage * rageItemAttackUpLowHealthMultiplayer);
                }
            }
            else
            {
                if (enemy.GetComponent<scr_EnemyDamaged>() != null)
                {
                    enemy.GetComponent<scr_EnemyDamaged>().Damage(totalDamage);
                }
                else if (enemy.GetComponentInChildren<scr_SunRaHands>() != null)
                {
                    enemy.GetComponentInChildren<scr_SunRaHands>().Damaged(totalDamage);
                }
                else if (enemy.GetComponentInChildren<scr_SunRaHands>() != null)
                {
                    enemy.GetComponentInChildren<scr_SunRaHands>().Damaged(totalDamage);
                }
                else if (enemy.GetComponentInChildren<scr_MainBody>() != null)
                {
                    enemy.GetComponentInChildren<scr_MainBody>().Damaged(totalDamage);
                }
            }
        }
    }

    public void SpawnFireWall()
    {
        // spawns firewall on the side the player is looking
        if (scr_PlayerMovement.instance.sp.flipX == true)
        {
            Instantiate(fireWall, new Vector2(scr_PlayerMovement.instance.tf.position.x + 1, scr_PlayerMovement.instance.tf.position.y + 0.65f), Quaternion.identity);
        }
        else
        {
            Instantiate(fireWall, new Vector2(scr_PlayerMovement.instance.tf.position.x - 1, scr_PlayerMovement.instance.tf.position.y + 0.65f), Quaternion.identity);
        }
    }

    // area to detect enemies around the attack point
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // calculation of how much damage to do
    public void calculateDamage()
    {
        totalDamage = baseDamage + damageUp;
    }

    public void OnDamaged()
    {
        // shakes the camera and creates HP amount changed at 0
        float playerHPAmountChanged = 0f;
        scr_CineMachineShake.Instance.ShakeCamera(5f, 0.1f);

        // takes away player HP
        if (rageItemInUse == true)
        {
            playerHealth -= enemyDamageAmount * rageItemEnemyAttackUpMultiplayer;
            playerHPAmountChanged = enemyDamageAmount * rageItemEnemyAttackUpMultiplayer;
        }
        else
        {
            playerHealth -= enemyDamageAmount;
            playerHPAmountChanged = enemyDamageAmount;
        }

        //player dead
        if (playerHealth <= 0)
        {
            // revives player if applicable
            if (isPlayerRevivable == true)
            {
                isPlayerRevivable = false;
                playerHPAmountChanged = maxPlayerHealth;
                playerHealth = maxPlayerHealth;
                scr_HealthBar.instance.SetHealth(maxPlayerHealth, playerHealth, -100f);
                scr_AudioManager.instance.player_Hit.start();
            }

            // player dies and game ends
            else
            {
                scr_AudioManager.instance.player_Dead.start();
                scr_HealthBar.instance.SetHealth(maxPlayerHealth, playerHealth, playerHPAmountChanged);
                scr_GameManager.instance.Lose();
            }
        }
        // player enters low health mode
        else if (playerHealth <= (maxPlayerHealth / 4))
        {
            isLowHealth = true;
            scr_HealthBar.instance.SetHealth(maxPlayerHealth, playerHealth, playerHPAmountChanged);
            scr_AudioManager.instance.player_Hit.start();
        }
        // sets HP bar and play sound
        else
        {
            scr_HealthBar.instance.SetHealth(maxPlayerHealth, playerHealth, playerHPAmountChanged);
            scr_AudioManager.instance.player_Hit.start();
        }
    }

    public void OnHealed()
    {
        // heals player and sets health bar value
        float healthDifference;
        if (playerHealth > maxPlayerHealth)
        {
            healthDifference = maxPlayerHealth - playerHealth;
            playerHealth = maxPlayerHealth;
            scr_HealthBar.instance.SetHealth(maxPlayerHealth, playerHealth, healthDifference);
        }

        // player exis low health mode
        if (playerHealth > maxPlayerHealth / 4)
        {
            isLowHealth = false;
            scr_HealthBar.instance.SetHealth(maxPlayerHealth, playerHealth, -10f);
        }
    }

    public void GameEnd()
    {
        // sets all variables to game over values
        isFireWallPickedUp = false;
        isPlayerRevivable = false;
        rageItemInUse = false;
        isLowHealth = false;
        canAttack = false;
        isDamageUp = false;
        rageItemCoolDown = 0;
        attackCoolDown = baseCoolDown;
        damageUpCoolDown = 0;
        playerHealth = 100;
        maxPlayerHealth = 100;
        enemyDamageAmount = 0;
        rageItemCoolDown = 0;
        damageUp = 0;
    }
}

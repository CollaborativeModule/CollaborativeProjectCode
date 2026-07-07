using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerMovement : MonoBehaviour
{

    public float playerSpeed, movement, playerSpeedUp, dashSpeedUp;
    public float numOfDashes, maxNumOfDashes, dashSpeed, dashCoolDown, dashRegenTime, playerSpeedUpTime, dashSpeedUpTime;
    public bool isPlayerDashed, isPlayerSpeedUp, isDashSpeedUp, hasDashed;

    public GameObject shield, shieldPrefab;
    public bool isShieldSpawned;
    public float shieldCoolDown;

    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public Transform tf;

    public Animator animtor;
    public bool isIdle, playerCanMove, isEndOfLevel, isEndOfBossLevel, canEndBossLevel;
    public float endOfLevelTimer, endOfLevelBaseTimer;

    public static scr_PlayerMovement instance;

    public float walkAudioDelay;

    public void Awake()
    {
        // sets value at game awake
        instance = this;
        endOfLevelTimer = endOfLevelBaseTimer;

        walkAudioDelay = 0.38f;
    }

    public void GameStart()
    {
        // sets values to game start state
        Destroy(shield);
        isDashSpeedUp = false;
        isPlayerSpeedUp = false;
        isDashSpeedUp = false;
        isShieldSpawned = false;
        isEndOfLevel = false;
        isPlayerDashed = false;
        hasDashed = false;
        canEndBossLevel = false;
        numOfDashes = 2;
        maxNumOfDashes = 2;
        dashCoolDown = 1;
        dashRegenTime = 5;
        playerSpeedUp = 0;
        dashSpeedUp = 0;
        playerSpeedUpTime = 0;
        dashSpeedUpTime = 0;
        shieldCoolDown = 0.5f;
        playerCanMove = true;
        endOfLevelTimer = endOfLevelBaseTimer;
    }

    public void GameEnd()
    {
        // sets values to game over state
        Destroy(shield);
        isDashSpeedUp = false;
        isPlayerSpeedUp = false;
        isDashSpeedUp = false;
        isShieldSpawned = false;
        isEndOfLevel = false;
        isPlayerDashed = false;
        hasDashed = false;
        canEndBossLevel = false;
        numOfDashes = 2;
        maxNumOfDashes = 2;
        dashCoolDown = 1;
        dashRegenTime = 5;
        playerSpeedUp = 0;
        dashSpeedUp = 0;
        playerSpeedUpTime = 0;
        dashSpeedUpTime = 0;
        shieldCoolDown = 0.5f;
        playerCanMove = false;
        endOfLevelTimer = endOfLevelBaseTimer;
    }

    
    void FixedUpdate()
    {
        // movement player based on input
        if (!hasDashed && scr_GameManager.instance.playerCanDoStuff == true)
        {
            rb.velocity = new Vector2(movement * (playerSpeed + playerSpeedUp) * Time.fixedDeltaTime, rb.velocity.y);
        }
        
    }

    public void LaunchUp(float playerLaunch)
    {
        // launches the player upwards
        rb.AddForce(transform.up * (playerLaunch));
    }

    void Update()
    {
        // player dash
        if (playerCanMove == true && scr_GameManager.instance.playerCanDoStuff == true)
        {
            // gets player input
            movement = Input.GetAxis("Horizontal");

            // player dashes based on input direction and plays dash sound
            if (Input.GetButtonDown("Dash") && numOfDashes > 0 && isPlayerDashed == false)
            {
                scr_AudioManager.instance.player_Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                scr_AudioManager.instance.player_Dash.start();
                walkAudioDelay = 0.38f;

                hasDashed = true;
                    if (sp.flipX == true)
                    {
                        rb.AddForce(transform.right * (dashSpeed + dashSpeedUp));
                }
                    else
                    {
                    rb.AddForce(-transform.right * (dashSpeed + dashSpeedUp));
                }
                numOfDashes = numOfDashes - 1;
                isPlayerDashed = true;
            }

            // flips player sprite and play animation, with a sound delay timer
            if (movement > 0)
            {
                sp.flipX = true;
                animtor.SetTrigger("Walking");
                walkAudioDelay -= Time.deltaTime;

                if (walkAudioDelay <= 0)
                {
                    walkAudioDelay = 0.38f;
                    scr_AudioManager.instance.player_Walk.start();
                }
            }

            // flips player sprite and play animation, with a sound delay timer
            else if (movement < 0)
            {
                sp.flipX = false;
                animtor.SetTrigger("Walking");
                walkAudioDelay -= Time.deltaTime;

                if (walkAudioDelay <= 0)
                {
                    walkAudioDelay = 0.38f;
                    scr_AudioManager.instance.player_Walk.start();
                }
            }

            // player enters idle animation and walk sound is stopped
            else if (movement == 0)
            {
                animtor.SetTrigger("Idle");
                scr_AudioManager.instance.player_Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                walkAudioDelay = 0.38f;
            }

            // destroy's current shield and spawns new shield
            if (Input.GetButton("Shield") && isShieldSpawned == false)
            {
                Destroy(shield);
                scr_AudioManager.instance.player_Shield.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                scr_AudioManager.instance.player_Shield.start();
                if (sp.flipX == true)
                {
                    Instantiate(shieldPrefab, new Vector3(tf.position.x + 1, tf.position.y + 0.553f, tf.position.z), Quaternion.identity, tf);
                }
                else
                {
                    Instantiate(shieldPrefab, new Vector3(tf.position.x - 1, tf.position.y + 0.553f, tf.position.z), Quaternion.identity, tf);
                }
                isShieldSpawned = true;
            }
        }

        // dash cool down usage
        if (isPlayerDashed == true)
        {
            dashCoolDown -= Time.deltaTime;

            // reset value to default to allow another dash
            if (dashCoolDown <= 0 )
            {
                hasDashed = false;
                dashCoolDown = 0.5f;
                isPlayerDashed = false;
            }
        }

        // dash regen timer
        if (numOfDashes < maxNumOfDashes)
        {
            dashRegenTime -= Time.deltaTime;

            // number of dashes available increases by 1 and resets timer
            if (dashRegenTime <= 0)
            {
                dashRegenTime = 5;
                numOfDashes += 1;
            }
        }

        // shield spawn timer
        if (isShieldSpawned == true)
        {
            shieldCoolDown -= Time.deltaTime;

            // resets values to default
            if (shieldCoolDown <= 0 )
            {
                shieldCoolDown = 0.5f;
                isShieldSpawned = false;
            }
        }

        // player speed up timer
        if (isPlayerSpeedUp == true)
        {
            scr_GameManager.instance.speedUp.SetActive(true);
            playerSpeedUpTime -= Time.deltaTime;
            scr_GameManager.instance.speedUpText.text = "" + Mathf.Round(playerSpeedUpTime);

            // resets values to default
            if (playerSpeedUpTime <= 0)
            {
                scr_GameManager.instance.speedUp.SetActive(false);
                playerSpeedUp = 0;
                isPlayerSpeedUp = false;
            }
        }

        // player dash up timer
        if (isDashSpeedUp == true)
        {
            scr_GameManager.instance.dashUp.SetActive(true);
            dashSpeedUpTime -= Time.deltaTime;
            scr_GameManager.instance.dashUpText.text = "" + Mathf.Round(dashSpeedUpTime);

            // resets values to default
            if (dashSpeedUpTime <= 0)
            {
                scr_GameManager.instance.dashUp.SetActive(false);
                dashSpeedUp = 0;
                isDashSpeedUp = false;
            }
        }

        if (isEndOfLevel == true)
        {
            rb.velocity = new Vector2(1f * playerSpeed * 20f * Time.deltaTime, rb.velocity.y); // moves player to the right
            endOfLevelTimer -= Time.deltaTime; // a timer which counts down to 0
            animtor.SetTrigger("Walking"); // triggers player animation to walking
            if (endOfLevelTimer <= 0 ) // when timer is 0 or less then do this
            {
                scr_GameManager.instance.NextLevel();
                isEndOfLevel = false;  // sets end of level is false so this code isn't done again
                endOfLevelTimer = endOfLevelBaseTimer; // timer is reset
                playerCanMove = true; // player is allowed to move again
                scr_GameManager.instance.playerCanDoStuff = true; // player is allowed to do things again
                animtor.SetTrigger("Idle"); // triggers player animation to idle
                scr_CineMachineCamMovement.instance.StartOfLevel();
            }
        }

        if (isEndOfBossLevel == true)
        {
            endOfLevelTimer -= Time.deltaTime;
            animtor.SetTrigger("");
            if (endOfLevelTimer <= 0)
            {
                canEndBossLevel = false;
                scr_GameManager.instance.NextLevel();
                isEndOfLevel = false;  // sets end of level is false so this code isn't done again
                endOfLevelTimer = endOfLevelBaseTimer; // timer is reset
                playerCanMove = true; // player is allowed to move again
                scr_GameManager.instance.playerCanDoStuff = true; // player is allowed to do things again
                animtor.SetTrigger("Idle"); // triggers player animation to idle
                scr_CineMachineCamMovement.instance.StartOfLevel();
            }
        }
    }

    public void EndOfLevel()
    {
        playerCanMove = false; // player can't move anymore
        isEndOfLevel = true; // level ends
        scr_GameManager.instance.playerCanDoStuff = false; // player can't do actions any more
        scr_CineMachineCamMovement.instance.EndOflevel();
    }

    public void EndOfBossLevel()
    {
        playerCanMove = false; // player can't move anymore
        isEndOfBossLevel = true;
        scr_GameManager.instance.playerCanDoStuff = false;
    }

    public void StartOfLevel()
    {
        // moves player to 0,0,0
        tf.position = Vector3.zero;
    }

}

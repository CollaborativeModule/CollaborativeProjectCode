using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerJump : MonoBehaviour
{
    public static scr_PlayerJump instance;
    public float playerJumpSpeed, numOfJumpsDone, numOfJumpsAllowed, checkRadius, playerJumpSpeedUp;
    public float playerJumpSpeedUpTimer;
    public bool isTouchingGround, isJumping, isPlayerJumpSpeedUp;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        // set variables to game start values
        numOfJumpsDone = 0;
        numOfJumpsAllowed = 2;
        playerJumpSpeedUp = 0f;
        playerJumpSpeedUpTimer = 0f;
        isJumping = false;
        isPlayerJumpSpeedUp = false;
        isTouchingGround = true;
    }

    public void GameEnd()
    {
        // set variables to game over values 
        numOfJumpsDone = 0;
        numOfJumpsAllowed = 2;
        playerJumpSpeedUp = 0f;
        playerJumpSpeedUpTimer = 0f;
        isJumping = false;
        isPlayerJumpSpeedUp = false;
        isTouchingGround = true; 
    }

    public void Update()
    {
        // checks if touching ground
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // allows jumping if touching ground
        if (!isTouchingGround)
        {
            isJumping = true;
        }

        // player jumps if the button is pressed
        if (Input.GetButtonDown("Jump") && numOfJumpsDone < numOfJumpsAllowed && scr_GameManager.instance.playerCanDoStuff == true)
        {
            numOfJumpsDone += 1;
            rb.velocity = new Vector2(rb.velocity.x, playerJumpSpeed + playerJumpSpeedUp);
            scr_AudioManager.instance.player_Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            scr_PlayerMovement.instance.walkAudioDelay = 0.38f;
            scr_AudioManager.instance.player_Jump.start();
        }

        // disallows jumping
        if (isJumping && isTouchingGround)
        {
            isJumping = false;
            numOfJumpsDone = 0;
        }

        // jump speed up timer
        if (isPlayerJumpSpeedUp == true)
        {
            scr_GameManager.instance.jumpUp.SetActive(true);
            playerJumpSpeedUpTimer -= Time.deltaTime;
            scr_GameManager.instance.jumpUpText.text = "" + Mathf.Round(playerJumpSpeedUpTimer);

            // reset values to default
            if (playerJumpSpeedUpTimer <= 0)
            {
                scr_GameManager.instance.jumpUp.SetActive(false);
                isPlayerJumpSpeedUp = false;
                playerJumpSpeedUp = 0f;
            }
        }
    }
}

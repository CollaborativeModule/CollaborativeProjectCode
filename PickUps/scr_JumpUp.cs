using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_JumpUp : MonoBehaviour
{
    public float jumpUpDelay, jumpUpAmount;
    public bool canInteract;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // player can interact
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // player can't interact
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    public void Update()
    {
        // player picks up item and item object is destroyed
        if (canInteract == true && Input.GetButtonDown("PickUp"))
        {
            scr_AudioManager.instance.pickup.start();
            scr_PlayerJump.instance.playerJumpSpeedUp = jumpUpAmount;
            scr_PlayerJump.instance.playerJumpSpeedUpTimer = jumpUpDelay;
            scr_PlayerJump.instance.isPlayerJumpSpeedUp = true;
            Destroy(gameObject);
        }
    }
}

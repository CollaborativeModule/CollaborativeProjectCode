using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SpeedUp : MonoBehaviour
{
    public float speedDelay, speedUpAmount;
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
        // player interact and item is picked up, then destroyed
        if (canInteract == true && Input.GetButtonDown("PickUp"))
        {
            scr_AudioManager.instance.pickup.start();
            scr_PlayerMovement.instance.playerSpeedUp += speedUpAmount;
            scr_PlayerMovement.instance.playerSpeedUpTime += speedDelay;
            scr_PlayerMovement.instance.isPlayerSpeedUp = true;
            Destroy(gameObject);
        }
    }
}

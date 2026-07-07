using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MaxHealthUp : MonoBehaviour
{
    public float healAmount, maxHealthIncreaseAmount;
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
            scr_PlayerCombat.instance.maxPlayerHealth += maxHealthIncreaseAmount;
            scr_PlayerCombat.instance.playerHealth += healAmount;
            scr_PlayerCombat.instance.OnHealed();
            Destroy(gameObject);
        }
    }
}

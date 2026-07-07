using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_NumOfDashesUp : MonoBehaviour
{
    public bool canInteract;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // allows player to interact
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // disallows player interaction
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    public void Update()
    {
        // if player can and does interact, item is picked up and instance is destroyed
        if (canInteract == true && Input.GetButtonDown("PickUp"))
        {
            scr_AudioManager.instance.pickup.start();
            scr_PlayerMovement.instance.maxNumOfDashes += 1;
            Destroy(gameObject);
        }
    }
}

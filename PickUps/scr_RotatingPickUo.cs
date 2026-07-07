using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_RotatingPickUo : MonoBehaviour
{
    public bool canInteract;
    public void OnTriggerEnter2D(Collider2D other)
    {
        // player can pick up
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // player can't pick up
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    public void Update()
    {
        // player picks up object and the object is destroyed
        if (canInteract == true && Input.GetButtonDown("PickUp"))
        {
            scr_AudioManager.instance.pickup.start();
            scr_RotatingPowerUp.instance.PowerUpPickedUp();
            Destroy(gameObject);
        }
    }
}

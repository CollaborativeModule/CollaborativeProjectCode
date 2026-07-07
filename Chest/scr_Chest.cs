using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Chest : MonoBehaviour
{
    public bool canInteract;

    public Transform tf;
    public GameObject item;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // allow player to interact with the chest
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // disallow plater interaction with the chest
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    public void Update()
    {
        // item spawn and chest deletion when interacted with
        if (canInteract == true && Input.GetButtonDown("PickUp"))
        {
            scr_ItemSpawnerManager.instance.spawnItemPos = new Vector3(tf.position.x, tf.position.y, tf.position.z);
            scr_ItemSpawnerManager.instance.SpawnItem();
            Destroy(gameObject);
        }
    }
}

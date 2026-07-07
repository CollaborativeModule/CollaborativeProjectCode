using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DestroyableBackground : MonoBehaviour
{
    public bool isTriggerEnter, isNotDestroyed, isFading;
    public float fadingSpeed, fadedAmount;
    public Sprite destroyedSprite;
    public SpriteRenderer sp;

    public float breakAbleEnvironmentType;

    public void Update()
    {
        // player can and does break object
        if (isTriggerEnter == true && Input.GetButtonDown("Fire1") && isNotDestroyed == false)
        {
            // prevents multi breaking same object
            isNotDestroyed = true;

            // play destruction sound
            scr_AudioManager.instance.environmentBreakAbles.setParameterByName("Breakable_Object", breakAbleEnvironmentType);
            scr_AudioManager.instance.environmentBreakAbles.start();

            // random chance of spawning an item
            float randomNum = Random.Range(0, 100);
            if (randomNum <= 10)
            {
                scr_ItemSpawnerManager.instance.spawnItemPos = transform.position;
                scr_ItemSpawnerManager.instance.SpawnItem();
            }

            // changes sprite to destroyed and invokes fade
            sp.sprite = destroyedSprite;
            Invoke("Fade", 3f);
        }

        // fades object out
        if (isFading == true)
        {
            sp.color = new Vector4 (1f,1f,1f, fadedAmount);
            fadedAmount -= fadingSpeed * Time.deltaTime;

            // once the object is fully faded, it's destroyed
            if (fadedAmount <= 0)
            {
                DestroyObject();
            }
        }
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        // player in trigger area
        if (other.CompareTag("Player") == true)
        {
            isTriggerEnter = true;
        }
    }
    
    public void OnTriggerExit2D (Collider2D other)
    {
        // player left trigger area
        if (other.CompareTag("Player") == true)
        {
            isTriggerEnter = false;
        }
    }

    public void Fade()
    {
        // starts fade out
        isFading = true;
    }

    public void DestroyObject()
    {
        // destroy instance
        Destroy(gameObject);
    }
}

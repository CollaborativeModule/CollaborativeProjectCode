using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Mimic : MonoBehaviour
{
    public bool playerCanInteract, interactedWithPlayer;
    public float damage;
    public Animator animator;

    public void Update()
    {
        if (Input.GetButtonDown("PickUp") && playerCanInteract == true && interactedWithPlayer == false) // conditions to see if interactible and what type of interaction
        {
            interactedWithPlayer = true; // makes it no longer interactable
            AttackPlayer(); // triggers function to attack the player
        }
        else if (Input.GetButtonDown("Fire1") && playerCanInteract == true && interactedWithPlayer == false)// conditions to see if interactible and what type of interaction
        {
            interactedWithPlayer = true; // makes it no longer interactable
            OnKilled(); // triggers death function
        }
    }

    public void OnTriggerEnter2D(Collider2D other) // when something enters the trigger box's area
    {
        if (other.CompareTag("Player")) // if the object which enters the trigger box area is player
        {
            playerCanInteract = true; // player can interact with mimic
            scr_AudioManager.instance.mimic_Enemy.setParameterByName("Enemies_State", 0f);
            scr_AudioManager.instance.mimic_Enemy.start();
        }
    }

    public void OnTriggerExit2D(Collider2D other) // when something exits the trigger box's area
    {
        if (other.CompareTag("Player"))// if the object which exits the trigger box area is player
        {
            playerCanInteract = false; // player can't interact with mimic
            scr_AudioManager.instance.mimic_Enemy.setParameterByName("Enemies_State", 0f);
            scr_AudioManager.instance.mimic_Enemy.start();
        }
    }

    public void OnKilled()
    {
        float randomSpawnItemChance = Random.Range(0, 100); // gives random number between 0 and 100
        if (randomSpawnItemChance <= 20) // if less then or equal to value stated then spawn item
        {
            scr_ItemSpawnerManager.instance.spawnItemPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            scr_ItemSpawnerManager.instance.SpawnItem();
        }
        Death(); // triggers death function
    }

    public void AttackPlayer()
    {
        scr_PlayerCombat.instance.enemyDamageAmount = damage; // tells player scripts damage amount
        scr_PlayerCombat.instance.OnDamaged(); // damages player script based on damage amount given
        animator.SetTrigger("Attack"); // plays attack animation
        scr_AudioManager.instance.mimic_Enemy.setParameterByName("Enemies_State", 1f);
        scr_AudioManager.instance.mimic_Enemy.start();
        Invoke("Death", 0.5f); // triggers death after playing attack animation
    }

    public void Death()
    {
        animator.SetTrigger("Death"); // plays death animation
        scr_AudioManager.instance.mimic_Enemy.setParameterByName("Enemies_State", 2f);
        scr_AudioManager.instance.mimic_Enemy.start();
        Destroy(gameObject, 0.5f); // after playing death animation, destroy's itself
    }
}

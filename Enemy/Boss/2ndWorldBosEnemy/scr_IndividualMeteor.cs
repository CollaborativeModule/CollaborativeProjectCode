using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_IndividualMeteor : MonoBehaviour
{
    public float movementSpeed;
    public bool isTouchingGround;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float checkRadius;

    public void Update()
    {
        // moves the meteor down in a \ or / pattern
        transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y - 3f * Time.deltaTime, transform.position.z);

        // checks if touching ground
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // if touching ground then destroy's itself
        if (isTouchingGround == true)
        {
            Destroy(gameObject);
        }
    }

        public void OnTriggerEnter2D(Collider2D other)
    {
        // if player collides, damage the player and then delete itself
        if (other.CompareTag("Player"))
        {
            scr_PlayerCombat.instance.enemyDamageAmount = script_SunRa.instance.baseDamage * 2;
            scr_PlayerCombat.instance.OnDamaged();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_FireWall : MonoBehaviour
{
    public float movement, fireWallSpeed, timeLeft, damage;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public void Update()
    {
        // timer, once done object is deleted
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 )
        {
            Destroy(gameObject);
        }

        // moves the firewall based on movement values and delta time
        rb.velocity = new Vector2(movement * fireWallSpeed * Time.deltaTime, rb.velocity.y);
    }

    public void Awake()
    {
        // adjust movement direction based on player look direction
        if (scr_PlayerMovement.instance.sp.flipX == true)
        {
            movement = 1;
        }
        else
        {
            movement = -1;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // damages collided enemy
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<scr_Enemy>().Damage(damage);
        }
    }
}

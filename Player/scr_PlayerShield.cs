using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerShield : MonoBehaviour
{
    public float timeLeft, health;
    public GameObject shield;

    void Update()
    {
        // shield timer
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 )
        {
            Destroy(gameObject);
        }
    }

    public void OnDamaged()
    {
        // if shield loses all it's destroyed
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

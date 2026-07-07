using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SunRaHands : MonoBehaviour
{
    public SpriteRenderer sp;
    public Sprite normal, destroyed;

    public void EndFirstPhase()
    {
        // sets sprite appearance, at end of phase 1
        sp.sprite = destroyed;
    }

    public void Damaged(float damageTaken)
    {
        // sends damage information to the overall boss script
        script_SunRa.instance.health -= damageTaken;
        script_SunRa.instance.Damage(damageTaken);
    }
}

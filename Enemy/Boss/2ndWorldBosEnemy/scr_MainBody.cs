using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MainBody : MonoBehaviour
{
    public SpriteRenderer sp;
    public Sprite normal, secondPhase;

    public void EndFirstPhase()
    {
        // sets sprite appearance, once in the second phase
        sp.sprite = secondPhase;
    }
    
    public void Damaged(float damageTaken)
    {
        // sends damage to the overall boss script
        script_SunRa.instance.health -= damageTaken;
        script_SunRa.instance.Damage(damageTaken);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_RotatingPowerUp : MonoBehaviour
{
    public static scr_RotatingPowerUp instance;
    public bool isRotating;
    public float baseTimer, timer;
    public GameObject rotatingItem;

    public void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        // orb timer
        if (isRotating == true)
        {
            scr_GameManager.instance.orbIcon.SetActive(true);
            timer -= Time.deltaTime;
            scr_GameManager.instance.orbIconText.text = "" + Mathf.Round(timer);

            // orb value reset and the orb disappears
            if (timer <= 0)
            {
                scr_GameManager.instance.orbIcon.SetActive(false);
                isRotating = false;
                timer = baseTimer;
                rotatingItem.SetActive(false);
            }
        }
    }

    public void PowerUpPickedUp()
    {
        // orb is activated and appears
        rotatingItem.SetActive(true);
        isRotating = true;
        scr_AudioManager.instance.player_Orb.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        scr_AudioManager.instance.player_Orb.start();
    }
}

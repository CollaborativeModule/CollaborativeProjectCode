using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_HealthBar : MonoBehaviour
{
    public Slider slider;
    public float newHealth, currentHealth, t;
    private bool healthSlider;

    public static scr_HealthBar instance;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        // slides HP bar to it's current value
        if (healthSlider == true)
        {
            slider.value = Mathf.Lerp(currentHealth, newHealth, t);
            t += Time.deltaTime;
            if (t > 1)
            {
                healthSlider = false;
                t = 0f;
                currentHealth = newHealth;
            }
        }
    }

    public void SetHealth(float maxHealth, float health, float newAmountChanged) // gets max health and current health
    {
        slider.maxValue = maxHealth; // sets max slider value to max health
        //slider.value = health; // sets value of slider to health
        newHealth = health;
        healthSlider = true;
        print(health);
    }

}

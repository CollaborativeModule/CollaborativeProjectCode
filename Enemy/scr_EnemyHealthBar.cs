using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_EnemyHealthBar : MonoBehaviour
{
    public GameObject enemy;
    public Slider slider;

    public float newHealth, currentHealth, t;
    private bool healthSlider;

    public static scr_EnemyHealthBar instance;

    public void Awake()
    {
        instance = this;
    }

        public void Update()
    {
        // if the slider exists, smoothly transition from the current enemy HP shown, to the true enemy HP value 
        if (healthSlider == true)
        {
            slider.value = Mathf.Lerp(currentHealth, newHealth, t);
            t += Time.deltaTime;

            // once the current HP is equal to the new HP, then the HP bar no longer moves
            if (t > 1)
            {
                healthSlider = false;
                t = 0f;
                currentHealth = newHealth;
            }
        }
    }

    public void bossEnemySetHealthBar()
    {
        // sets the value's for the HP bar
        slider = scr_GameManager.instance.bossHealthBar.GetComponent<Slider>();
        enemy.GetComponent<scr_EnemyDamaged>().Damage(0f);
    }

    public void SetHealth(float maxHealth, float health)
    {
        // sets the HP bar value's
        slider = scr_GameManager.instance.bossHealthBar.GetComponent<Slider>();
        slider.maxValue = maxHealth;
        if (slider != null)
        {
            newHealth = health;
            healthSlider = true;
        }

        // deactivate HP bar when the boss is dead
        if (health == 0)
        {
            scr_GameManager.instance.bossHealthBar.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : HealthManager
{
    public Vector3 healthBar;
    public GameObject overallHealthBar;
    public GameObject healthBars;
    public GameObject healthBarsBackground;
    public Vector3 healthBarsBackgroundScale;

   
    void Start()
    {
        // sets healthBar vector3 to the scale of the healthbar
        healthBar = healthBars.transform.localScale;

        // sets the background vector3 to the scale of the background
        healthBarsBackgroundScale = healthBarsBackground.transform.localScale;

    }

    public void Update()
    {
        checkHealthBars();
        checkDeath();

    }

    public void checkHealthBars()
    {
        if (gameObject.tag == "Slime")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
        if (gameObject.tag == "Boar")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
        if (gameObject.tag == "eyeDemon")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
        //if the tag is a boss
        if (gameObject.tag == "SlothBoss")
        {
            healthBar.y = 20f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * .25f;
            healthBarsBackgroundScale.x = healthMax * .25f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
        if (gameObject.tag == "Bat")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 10f;
            healthBarsBackgroundScale.x = healthMax * 10f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;


        }
        if (gameObject.tag == "Skellyboy")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
        //if its the player
        if (gameObject.tag == "Player")
        {
            healthBar.y = 50f;
            healthBarsBackgroundScale.y = 55f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;

            healthBars.transform.localScale = healthBar;

            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
    }
}


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
        // checks the healthbars on every frame update
        checkHealthBars();

        // checks if theres a death
        checkDeath();

    }

    public void checkHealthBars()
    {
        // handles the healthbar for slimes 
        if (gameObject.tag == "Slime" || gameObject.tag == "Large Slime")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
        // handles the healthbar for boars
        if (gameObject.tag == "Boar")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }

        // handles the healthbar for eye demons
        if (gameObject.tag == "eyeDemon")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
        
        // handles the healthbar for bats
        if (gameObject.tag == "Bat")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 10f;
            healthBarsBackgroundScale.x = healthMax * 10f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;


        }

        // handles the healthbar for skeletons
        if (gameObject.tag == "Skellyboy")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 3f;
            healthBarsBackgroundScale.x = healthMax * 3f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }

        // handles the healthbar for king slime
        if (gameObject.tag == "King Slime")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 1.5f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }

        // handles the healthbar for spicy bois
        if (gameObject.tag == "Spicy Boi")
        {
            healthBar.y = 25f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 10f;
            healthBarsBackgroundScale.x = healthMax * 10f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }
        //handles the healthbar for Gluttony
        if (gameObject.tag == "Gluttony")
        {
            healthBar.y = 35f;
            healthBarsBackgroundScale.y = 20f;
            healthBar.x = health * 1.5f;
            healthBars.transform.localScale = healthBar;
            healthBarsBackground.transform.localScale = healthBarsBackgroundScale;
        }

        // if its the player, and handles the healthbar for the player
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


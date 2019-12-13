using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour
{
    public int health = 100;
    public int healthMax = 100;
    Player player;


    void Start()
    {
        // on start set health to their max health to reset values
        health = healthMax;
    }
  
    void Update()
    {
        checkDeath();
    }

    public void checkDeath()
    {
        //if health goes over max, reset it to the max
        if (health > healthMax)
        {
            health = healthMax;
        }

        //destroys object upon losing all health
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //returns the health value
    public int GetHealth()
    {
        return health;
    }


    //subtract damage from health
    public void Damage(int damageAmount)
    {

        health -= damageAmount;
        Player.PlayerHealth = health;

    }

    //add health to the total amount of health
    public void Heal(int healAmount)
    {
        health += healAmount;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

}

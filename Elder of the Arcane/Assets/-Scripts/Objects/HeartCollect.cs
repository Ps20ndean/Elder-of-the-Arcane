using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollect : MonoBehaviour
{
    private GameObject player;
    HealthManager healthManager;

    private void Start()
    {
        player = GameObject.Find("Player");
        var heartBox = this.GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        // heartbox is equal to the boxcollider of the object
        var heartBox = this.GetComponent<BoxCollider2D>();

        //Checks if player exists. If it does, proceed.
        if (player != null)
        {
            //this is set to true if the players health is less than the max
            var playerHealthLessThanMax = player.GetComponent<HealthManager>().health < player.GetComponent<HealthManager>().healthMax; 
            if (playerHealthLessThanMax)
            {
                // if the player has less than max health enable the hitbox
                heartBox.enabled = true;
            }
            else
            {
                //if the player has max health then disable the hitbox
                heartBox.enabled = false;
            }
        }
    }
        public void OnCollisionEnter2D(Collision2D collision)
    {
        // playercomp is equal to the projectile attack script attached to the player
        var playercomp = player.GetComponent<ProjectileAttack>();

        // if collision with the player do the following
            if (collision.gameObject.tag == "Player")
            {

            // heal the player by 75 when you collect a heart
            player.GetComponent<HealthManager>().Heal(75);
           
            // destroys the gameobject on collecting
            Destroy(gameObject);

            // adds a heal charge to the players available charges
            playercomp.earthChargeAmounts += 1;
            }
        }

    }


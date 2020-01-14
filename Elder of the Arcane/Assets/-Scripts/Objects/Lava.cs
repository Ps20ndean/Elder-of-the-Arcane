using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // player is equal to the player gameobject
        var player = GameObject.Find("Player");

        // gets the player script attached to the player object
        var playerComp = player.GetComponent<Player>();

        if (collision.gameObject.tag == "Player")
        {
            // kills the player on touching the lava
            player.GetComponent<HealthManager>().health = 0;
            
            // runs the dead method upon dying
            playerComp.Dead();
        }
    }
}


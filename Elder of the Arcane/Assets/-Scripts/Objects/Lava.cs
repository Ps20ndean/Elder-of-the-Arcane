using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = GameObject.Find("Player");
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<HealthManager>().health = 0;
        }
    }
}


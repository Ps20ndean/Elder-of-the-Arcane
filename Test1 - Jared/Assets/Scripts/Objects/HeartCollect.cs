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
        var heartBox = this.GetComponent<BoxCollider2D>();
        var playerHealthLessThanMax = player.GetComponent<HealthManager>().health < player.GetComponent<HealthManager>().healthMax;
        if (playerHealthLessThanMax)
        {
            heartBox.enabled = true;
        }
        else
        {
            heartBox.enabled = false;
        }
    }
        public void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "Player")
            {
                    player.GetComponent<HealthManager>().Heal(75);
                    Destroy(gameObject);
            }
        }

    }


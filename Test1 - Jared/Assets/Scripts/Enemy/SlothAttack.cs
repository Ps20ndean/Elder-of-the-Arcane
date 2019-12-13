using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
    public class SlothAttack : EnemyAI
{
    private bool slothAttacked = false;
    private bool slothAttacking;
    new void Start()
    {
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        if (facingRight)
        {
            intFacingRight = -1;
        }
        else if (!facingRight)
        {
            intFacingRight = 1;
        }
        Distance();
        if (target)
        {
            dist = Math.Abs(Vector3.Distance(target.position, transform.position));
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        // Boss Parameters
        if (target)
        {
            if (gameObject.tag == "SlothBoss" && gameObject.GetComponent<HealthManager>().health < gameObject.GetComponent<HealthManager>().healthMax)
            {
                movementSpeed = .2f;
                anime.SetBool("Awake", true);
                transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
                if (movement && inDist)
                {
                    if ((player.transform.position.x < transform.position.x) && facingRight == true && inDist)
                    {
                        transform.Rotate(Vector3.up * 180);
                        facingRight = false;
                    }
                    else if ((player.transform.position.x >= transform.position.x) && facingRight == false && inDist)
                    {
                        transform.Rotate(Vector3.up * 180);
                        facingRight = true;
                    }
                }
            }
        }
    } 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(30, 15, 35, collision);
    }
}

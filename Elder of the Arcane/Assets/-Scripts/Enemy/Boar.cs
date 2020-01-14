using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boar : EnemyAI
{

    new void Start()
    {
        // Set gameobjects to their respective object
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        //Constantly checking if player is in distance
        Distance();
        
        if (target) { 
        if (movement && inDist)
        {
                // if player is in distance then play animation of moving
            anime.SetBool("Attacking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        } else
        {
                // once player is out of distance then set its animations to nothing
            anime.SetBool("Attacking", false);
        }

        if ((target.position.x < transform.position.x) && facingRight == true && inDist)
        {
                // is able to flip the enemy to the correct position
            transform.Rotate(Vector3.up * 180);
            facingRight = false;
            Wait(1);
        }
        else if ((target.position.x > transform.position.x) && facingRight == false && inDist)
        {
                // is able to flip the enemy to the correct position
            transform.Rotate(Vector3.up * 180);
            facingRight = true;
            Wait(1);
        }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // takes 30 fireball damage, 15 ice damage, deals 20 damage, gives player 20 score on death
        TakeDamage(30, 15, 20, 20, collision);
    }
}

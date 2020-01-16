using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skeleton : EnemyAI
{
    //Makes variables for Skeleton
    public float attackingDist = 2f;
    private bool isWaiting = false;


    new void Start()
    {
        // sets the parameters to the respective values
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        //Checks distance between player and skeleton
        Distance();

        //Check if player exists
        if (target)
        {
            //Checking if skeleton is close enough to attack, else keep moving
            if (attackingDist >= dist)
            {
                //play skeleton attacking animation
                anime.SetBool("Attacking", true);
            }
            else
            {
                anime.SetBool("Attacking", false);
                //give skeleton movement abilities again
                movement = true;
                canJump = true;
                //if within distance of player, move towards the player
                if (movement && inDist)
                {
                   transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
                   //play skeleton walking animation
                   anime.SetBool("Walking", true);
                }
                else
                {
                    //stop skeleton walking animation
                    anime.SetBool("Walking", false);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if (gameObject.tag == "SkellyBoss")
        {
            TakeDamage(30, 15, 120, 2300, collision);
        }
        else
        {
            // takes 30 fireball damage, takes 15 ice damage, deals 120 damage (kinda overkill but okay), and adds 20 score to players count
            TakeDamage(30, 15, 120, 20, collision);
        }
    }
}

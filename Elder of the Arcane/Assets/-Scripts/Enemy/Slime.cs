using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slime : EnemyAI
{

    private new void Start()
    {
        // Set gameobjects to their respective object
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        // checks if player is in distance of enemy
        Distance();
        if (movement && inDist && !isJumping)
        {
            // if the slime isnt jumping, has movement, and the player is in distance then play the animation
            anime.SetBool("SlimeJump", true);
        }
        else
        {
            // else if one of them is false then reset the animation
            anime.SetBool("SlimeJump", false);
        }

        //if target exists then do the following
        if (target)
        {
         
            // if the slime is not jumping then do the following
        if (myRigidBody.velocity.y != 0 && inDist && !isJumping)
        {
                // this makes the slime jump
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 7.25f, 0);
                // sets the jumping value to true
                isJumping = true;
                // starts the method WaitJump
            StartCoroutine(WaitJump());
        }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "King Slime")
        {
            // if its a king slime then it takes 30 fireball damage, 15 ice damage, deals 64 damage, and gives player 100 score on death
            TakeDamage(45, 15, 64, 1500, collision);
        }
        if (gameObject.tag == "Slime")
        {
            // if its a normal small slime then take 25 fireball damage, takes 15 ice damage, deals 25 damage to player, and adds 10 score to player on death 
            TakeDamage(25, 15, 25, 10, collision);
        }
        if (gameObject.tag == "Large Slime")
        {
            // if a large slime, then take 35 fireball damage, 15 ice damage, deals 50 damage and adds 25 score on death
            TakeDamage(35, 15, 50, 25, collision);
        }
    }
    IEnumerator WaitJump()
    {

        if (isJumping)
        {
            // if the slime is jumping then enable the animator
            anime.enabled = true;
        }
        else if (!isJumping)
        {
            // if the slime isnt jumping then stop the animator
            anime.enabled = false;
        }
        // wait 2 seconds
        yield return new WaitForSeconds(2);

        // sets is jumping to false as to signal that its on the ground
        isJumping = false;
    }
  
}

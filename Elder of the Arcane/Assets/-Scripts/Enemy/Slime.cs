using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slime : EnemyAI
{

    private new void Start()
    {
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        Distance();
        if (movement && inDist)
        {
            anime.SetBool("SlimeJump", true);
        }
        else
        {
            anime.SetBool("SlimeJump", false);
        }
        if (target){ 
        if (myRigidBody.velocity.y != 0 && inDist && !isJumping)
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 7.25f, 0);
                
            isJumping = true;
            StartCoroutine(WaitJump());
        }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        TakeDamage(30, 15, 25, 10, collision);
    }
    IEnumerator WaitJump()
    {
        if (movement)
        {
            isJumping = true;
        }
        else if (!movement)
        {
            isJumping = false;
        }
        movement = true;
        
        yield return new WaitForSeconds(2);
        movement = false;
        isJumping = false;
        anime.enabled = true;
    }
  
}

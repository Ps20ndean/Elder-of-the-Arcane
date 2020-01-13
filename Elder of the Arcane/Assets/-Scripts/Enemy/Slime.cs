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
        if (movement && inDist && !isJumping)
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
        if (gameObject.tag == "King Slime")
        {
            TakeDamage(30, 15, 64, 10, collision);
        }
        if (gameObject.tag == "Slime")
        {
            TakeDamage(30, 15, 25, 10, collision);
        }
        if (gameObject.tag == "Large Slime")
        {
            TakeDamage(30, 15, 50, 10, collision);
        }
    }
    IEnumerator WaitJump()
    {

        if (isJumping)
        {
            anime.enabled = true;
        }
        else if (!isJumping)
        {
            anime.enabled = false;
            Debug.Log("Ugh");
        }
        yield return new WaitForSeconds(2);
        isJumping = false;
    }
  
}

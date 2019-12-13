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
        if (movement && inDist && movement)
        {
            anime.SetBool("SlimeJump", true);
        }
        else
        {
            anime.SetBool("SlimeJump", false);
        }
        if (target){ 
        if ((target.position.y >= transform.position.y) && myRigidBody.velocity.y == 0 && (Math.Abs(target.position.x - this.transform.position.x) < 20) && inDist && !isJumping)
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 7.25f, 0); ;
            StartCoroutine(WaitJump());
        }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        TakeDamage(30, 15, 25, collision);
    }
    IEnumerator WaitJump()
    {
        isJumping = true;
        if (isJumping)
        {
            GetComponent<Animation>().enabled = true;
        }
        else if (!isJumping)
        {
            GetComponent<Animation>().enabled = false;
        }
        yield return new WaitForSeconds(2);

        isJumping = false;
    }
  
}

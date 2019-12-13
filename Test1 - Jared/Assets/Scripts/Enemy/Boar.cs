using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boar : EnemyAI
{

    new void Start()
    {
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        Distance();
        
        if (target) { 
        if (movement && inDist)
        {
            anime.SetBool("Attacking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        } else
        {
            anime.SetBool("Attacking", false);
        }

        if ((target.position.x < transform.position.x) && facingRight == true && inDist)
        {
            transform.Rotate(Vector3.up * 180);
            facingRight = false;
        }
        else if ((target.position.x > transform.position.x) && facingRight == false && inDist)
        {
            transform.Rotate(Vector3.up * 180);
            facingRight = true;
        }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(30, 15, 20, collision);
    }
}

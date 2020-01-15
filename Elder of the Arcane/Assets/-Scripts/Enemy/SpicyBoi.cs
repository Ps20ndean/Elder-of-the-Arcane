using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpicyBoi : EnemyAI
{

    new void Start()
    {
        // sets the parameters to the respective values
        enemyParameterCheck();
        if (target){
            if (target.position.x > transform.position.x)
            {
                transform.Rotate(Vector3.up * 180);
                facingRight = true;
            }
        }
        
    }

    // Update is called once per frame
    new void Update()
    {
        // checks if the enemy is within distance of the player
        Distance();

        if (target) // if the target exists do the following
        {
            if (movement && inDist)
            {
                // if within distance then set the animation to true
                anime.SetBool("Attacking", true);

                // runs towards player
                transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            }
            else
            {
                // if not within distance or has no movement then disable the animation
                anime.SetBool("Attacking", false);
            }

            if ((target.position.x < transform.position.x) && facingRight == true && inDist)
            {
                // flips the enemy
                transform.Rotate(Vector3.up * 180);
                facingRight = false;
                Wait(1);
            }
            else if ((target.position.x > transform.position.x) && facingRight == false && inDist)
            {
                // flips the enemy
                transform.Rotate(Vector3.up * 180);
                facingRight = true;
                Wait(1);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // takes 10 fireball damage, 30 ice damage, deals 20 damage, and gives the player 20 score when it dies
        TakeDamage(30, 30, 25, 20, collision);
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator Wait(float delayInSecs)
    {
        // waits a certain amount of seconds
        yield return new WaitForSecondsRealtime(delayInSecs);
    }
}

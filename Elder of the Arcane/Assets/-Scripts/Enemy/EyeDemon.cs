using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EyeDemon : EnemyAI
{

    new void Start()
    {
        // sets the parameters to the respective values
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        // checks if the player is in distance
        Distance();



        if (!inDist) // if its not in distance then set chase to false
        {
            anime.SetBool("Chase", false);

        } else // if in distance then set bool chase to true to enable animations
        {
            anime.SetBool("Chase", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // takes 30 fireball damage, takes 15 ice damage, deals 30 damage to player, and gives 25 score to player
        TakeDamage(30, 15, 30, 25, collision);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bat : EnemyAI
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Takes fire, and ice damage, able to give damage to player, and gives score on death.
        TakeDamage(30, 15, 20, 10, collision);
    }
}

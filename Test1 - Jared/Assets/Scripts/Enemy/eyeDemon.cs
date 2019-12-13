using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class eyeDemon : EnemyAI
{

    new void Start()
    {
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        Distance();
         if (!inDist)
        {
            anime.SetBool("Chase", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(30, 15, 30, collision);
    }
}

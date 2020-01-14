using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier : EnemyAI
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // takes 30 fireball damage, takes 15 ice, deals 20 damage to player, gives no score on death
        TakeDamage(15, 30, 20, 0, collision);
    }
}

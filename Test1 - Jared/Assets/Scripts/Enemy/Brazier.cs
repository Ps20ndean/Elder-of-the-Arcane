using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier : EnemyAI
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(30, 15, 20, collision);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skellyboy : EnemyAI
{
    // Start is called before the first frame update
    new void Start()
    {
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        Distance();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(30, 15, 20, collision);
    }
}

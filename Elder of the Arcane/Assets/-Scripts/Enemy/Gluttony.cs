using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gluttony : EnemyAI
{
    //Makes variables for Gluttony
    public float attackingDist = 2f;
    private bool isWaiting = false;
    private float varNum;
    new void Start()
    {
        // sets the parameters to the respective values
        enemyParameterCheck();
        
    }

    // Update is called once per frame
    new void Update()
    {
        //Checks distance between player and gluttony

        //Check if player exists
        if (target)
        {
            varNum = Random.Range(0, 6);
            if (varNum < 4)
            {
                anime.SetBool("Stomping (5)", false);
                anime.SetBool("Charging (4)", false);
                Wait(2);
            }
            else if (varNum == 4){
                anime.SetBool("Stomping (5)", false);
                anime.SetBool("Charging (4)", true);
                movement = true;
                Distance();
                Wait(2);
                movement = false;
                Wait(2);

            }
            else if (varNum == 5)
            {
                anime.SetBool("Stomping (5)", true);
                anime.SetBool("Charging (4)", false);

            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // takes 30 fireball damage, takes 15 ice damage, deals 120 damage (kinda overkill but okay), and adds 20 score to players count
        TakeDamage(15, 30, 60, 5200, collision);
    }
}

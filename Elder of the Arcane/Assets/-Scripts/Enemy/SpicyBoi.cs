using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpicyBoi : EnemyAI
{

    new void Start()
    {
        enemyParameterCheck();
    }

    // Update is called once per frame
    new void Update()
    {
        Distance();

        if (target)
        {
            if (movement && inDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            }
            else
            {  
            }

            if ((target.position.x < transform.position.x) && facingRight == true && inDist)
            {
                transform.Rotate(Vector3.up * 180);
                facingRight = false;
                Wait(1);
            }
            else if ((target.position.x > transform.position.x) && facingRight == false && inDist)
            {
                transform.Rotate(Vector3.up * 180);
                facingRight = true;
                Wait(1);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(30, 15, 20, 20, collision);
    }
    public IEnumerator Wait(float delayInSecs)
    {
        yield return new WaitForSecondsRealtime(delayInSecs);
    }
}
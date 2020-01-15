using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
    [RequireComponent(typeof(EnemyController))]
public class EnemyAI : HealthBar
{
    //Makes variables
    public bool movement = true;
    public float movementSpeed = 2.0f;
    public float stoppingDistance = 13f;
    public bool facingRight = true;
    public bool canJump = true;
    [HideInInspector]
    public Rigidbody2D myRigidBody;
    [HideInInspector]
    public Animator anime;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public GameObject player;
    public Vector3 velocity;
    public bool isJumping = false;
    public float dist;
    public bool inDist;

    public int intFacingRight;
    public new void Update()
    {
        if (facingRight)
        {
            // allows enemies to switch direction
            intFacingRight = -1;
        }
        else if (!facingRight)
        {
            // allows enemies to switch direction
            intFacingRight = 1;
        }
    }
    public void Start()
    {
        // sets to animator component of the gameobject
        anime = GetComponent<Animator>();

        // target gets set to player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // rigidbody is set to var myRigidBody
        myRigidBody = GetComponent<Rigidbody2D>();
        
    }
    public void Distance()
    {
        // if target exists
        if (target)
        { 
            // dist is equal to the distance between player and enemy
            dist = Math.Abs(Vector3.Distance(target.position, transform.position));
            
        // if dist is less than the stoppingdistance
         if (dist <= stoppingDistance)
         {
                //sets indist to true which allows it to move
            inDist = true;
         }
            else if (dist > stoppingDistance)
         {
                //sets indist to false which stops the enemy from moving
                inDist = false;
         }

            if (movement && inDist)
            {
                // this is what moves the enemy towards the player
               transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            }

            if ((target.position.x < transform.position.x) && facingRight == true && inDist)
            {
                // flips the enemy
                transform.Rotate(Vector3.up * 180);
                facingRight = false;
            }
            else if ((target.position.x > transform.position.x) && facingRight == false && inDist)
             {
                // flips the enemy
                transform.Rotate(Vector3.up * 180);
                facingRight = true;
             }
        }
    }

    public IEnumerator WaitMov(float Seconds)
    {
        // wait for a certain number of seconds
        yield return new WaitForSeconds(Seconds);
        movement = true;
    }

    // allows for more flexible taking of damage, giving damage, and giving score
    public void TakeDamage(int FireDamage, int IceDamage, int PlayerDamage, int AddScore, Collision2D collision)
    {
        // if the player exists do the following
        if (player != null)
        {   // sets playerComp to the Player script attached to the player
            var playerComp = player.GetComponent<Player>();

            // if colliding with the player
            if (collision.gameObject.tag == "Player")
            {
                //gets component of healthmanager and takes a certain amount of damage then killing if health = 0
                player.GetComponent<HealthManager>().Damage(PlayerDamage);
                playerComp.Dead();
                
                
                //sets movement to false
                movement = false;

                // waits a second
                WaitMov(1f);

                //sets movement to true
                movement = true;
            }

            if (collision.gameObject.tag == "FireBall")
                // if colliding with a fireball take damage
            {
                GetComponent<HealthBar>().Damage(FireDamage);
            }

            if (collision.gameObject.tag == "Ice")
            {
                // if colliding with ice then take damage
                GetComponent<HealthManager>().Damage(IceDamage);
            }
            // logs events of damage to the event log text file
            string path = "Logs/EventLog.txt";
            
            // once the gameobject dies then do the following
            if (gameObject.GetComponent<HealthManager>().health <= 0)
            {
                // add score based on what is set on the enemy to the players score
                playerComp.scoreInt += AddScore;
                playerComp.SavePlayer();
                if (gameObject.tag == "King Slime") {
                    // once the king slime is dead then add the event "king slime killed"
                    File.AppendAllText(path, " King Slime Killed");
                } else if (gameObject.tag == " SkellyBoss") { 
                    // once the skeleton boss is killed then add the event "skeleton boss killed"
                    File.AppendAllText(path, " Skeleton Boss Killed");
                } else
                {
                    // else if a normal enemy is killed add that an enemy died
                    File.AppendAllText(path, " Enemy Killed");
                }
            }
        }
    }
    // this allows for easier and more user friendly use of code
    public void enemyParameterCheck()
    {
        // sets player to correct gameobject
        player = GameObject.Find("Player");
        //sets target to the transform component of player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // sets anime to the animator component
        anime = GetComponent<Animator>();
        // myRigidBody is the rigidbody2d component
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public IEnumerator Wait(float delayInSecs)
    {
        // wait a certain number of seconds
        yield return new WaitForSeconds(delayInSecs);
    }

   
}

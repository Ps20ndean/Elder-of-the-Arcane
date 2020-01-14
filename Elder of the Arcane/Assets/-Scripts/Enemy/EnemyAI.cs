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
    public void TakeDamage(int FireDamage, int IceDamage, int PlayerDamage, int AddScore, Collision2D collision)
    {
        // if the player exists do the following
        if (player != null)
        {   // sets playerComp to the Player script attached to the player
            var playerComp = player.GetComponent<Player>();

            // if colliding with the player
            if (collision.gameObject.tag == "Player")
            {
                //gets component of healthmanager and takes a certain amount of damage
                player.GetComponent<HealthManager>().Damage(PlayerDamage);
                
                //sets movement to false
                movement = false;

                // waits a second
                WaitMov(1f);

                //sets movement to true
                movement = true;
            }

            if (collision.gameObject.tag == "FireBall")
            {
                GetComponent<HealthBar>().Damage(FireDamage);
            }
            if (collision.gameObject.tag == "Ice")
            {
                GetComponent<HealthManager>().Damage(IceDamage);
            }
            string path = "Logs/EventLog.txt";
            if (gameObject.GetComponent<HealthManager>().health <= 0)
            {
                playerComp.scoreInt += AddScore;
                if (gameObject.tag == "King Slime") {
                    File.AppendAllText(path, " King Slime Killed");
                } else if (gameObject.tag == " SkellyBoss") { 
                    File.AppendAllText(path, " Skeleton Boss Killed");
                } else
                {
                    File.AppendAllText(path, " Enemy Killed");
                }
            }
        }
    }
    public void enemyParameterCheck()
    {
        player = GameObject.Find("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anime = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public IEnumerator Wait(float delayInSecs)
    {
        yield return new WaitForSecondsRealtime(delayInSecs);
    }
}

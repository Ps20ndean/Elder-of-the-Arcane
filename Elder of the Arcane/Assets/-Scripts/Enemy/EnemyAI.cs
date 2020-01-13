using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
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
    SpriteRenderer sr;
    Color defaultColor;
    public int intFacingRight;

    public new void Update()
    {
        //finds out if object is facing right or not. Adds values to variables for later use.
        if (facingRight)
        {
            intFacingRight = -1;
        }
        else if (!facingRight)
        {
            intFacingRight = 1;
        }
    }
    public void Start()
    {
        //sets variables for object when called for later use.
        anime = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Grabbing the SpriteRenderer and creating variable to store the color
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;

        myRigidBody = GetComponent<Rigidbody2D>();
        
    }
    public void Distance()
    {
        //check if player exists
        if (target)
        { 
            //puts the distance between player and object in the dist variable
            dist = Math.Abs(Vector3.Distance(target.position, transform.position));
         //checks if the distance between player and object is or is not within "attacking" distance
         if (dist <= stoppingDistance)
         {
            //sets variable for later use
            inDist = true;
         }
        else if (dist > stoppingDistance)
         {
            //sets variable for later us
            inDist = false;
         }

            //checks if object is able to move and close to player
            if (movement && inDist)
            {
                //begins moving object towards the player
               transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            }

            //checks which side the player is in relation to object. Flips sides accordingly to face player.
            if ((target.position.x < transform.position.x) && facingRight == true && inDist)
            {
                transform.Rotate(Vector3.up * 180);
                facingRight = false;
            }
            else if ((target.position.x > transform.position.x) && facingRight == false && inDist)
             {
                transform.Rotate(Vector3.up * 180);
                facingRight = true;
             }
        }
    }

    //when called, object cannot move for given amount of seconds
    public IEnumerator WaitMov(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        movement = true;
    }

    //On collision, deals or receives damage and adds score if needed
    public void TakeDamage(int FireDamage, int IceDamage, int PlayerDamage, int AddScore, Collision2D collision)
    {
        //checks if player exists
        if (target)
        {
            //creates a variable for future use
            var playerComp = player.GetComponent<Player>();

            //if collision with player, deal damage, make player invincible temporarily, and stop movement temporarily
            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<HealthManager>().Damage(PlayerDamage);
                startInvinc(20);
                //if hit, switch color of player to show invincibility
                movement = false;
                WaitMov(1f);
                movement = true;
            }

            //changes health bar values depending on damage type
            if (collision.gameObject.tag == "FireBall")
            {
                GetComponent<HealthBar>().Damage(FireDamage);
            }
            else if (collision.gameObject.tag == "Ice")
            {
                GetComponent<HealthManager>().Damage(IceDamage);
            }

            //if object's health is less or equal to 0, DIE.
            if (gameObject.GetComponent<HealthManager>().health <= 0)
            {
                //adds score to the player's coin purse
                playerComp.scoreInt += AddScore;
            }
        }
    }
    //grants invincibility to player and changes color to signify that
    IEnumerator startInvinc(float seconds)
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        sr.color = new Color(128f, 128f, 128f);
        yield return new WaitForSeconds(seconds);
        sr.color = defaultColor;
        Physics2D.IgnoreLayerCollision(8, 10, false);
    }
    //find player's position
    public void enemyParameterCheck()
    {
        player = GameObject.Find("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anime = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }
}
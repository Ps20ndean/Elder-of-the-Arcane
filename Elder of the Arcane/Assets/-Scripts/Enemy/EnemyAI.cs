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

    public int intFacingRight;
    public new void Update()
    {
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
        anime = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        myRigidBody = GetComponent<Rigidbody2D>();
        
    }
    public void Distance()
    {
        if (target)
        { 
            dist = Math.Abs(Vector3.Distance(target.position, transform.position));
         if (dist <= stoppingDistance)
         {
            inDist = true;
         }
            else if (dist > stoppingDistance)
         {
                inDist = false;
         }

            if (movement && inDist)
            {

               transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            }

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

    public IEnumerator WaitMov(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        movement = true;
    }
    public void TakeDamage(int FireDamage, int IceDamage, int PlayerDamage, int AddScore, Collision2D collision)
    {
        if (player != null)
        {
            var playerComp = player.GetComponent<Player>();



            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<HealthManager>().Damage(PlayerDamage);
                startInvinc(20);
                movement = false;
                WaitMov(1f);
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
            if (gameObject.GetComponent<HealthManager>().health <= 0)
            {
                playerComp.scoreInt += AddScore;
            }
        }
    }
    IEnumerator startInvinc(float seconds)
    {
        //var alpha = player.GetComponent<SpriteRenderer>().color.a;
        Physics2D.IgnoreLayerCollision(8, 10, true);
       // alpha = .75f;                 doesnt work, fix this tomorrow
        yield return new WaitForSeconds(seconds);
        Physics2D.IgnoreLayerCollision(8, 10, false);
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

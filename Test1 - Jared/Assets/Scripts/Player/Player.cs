﻿using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
// to close game Application.Quit();
// UnityEditor.EditorApplication.isPlaying = false;



public class Player : MonoBehaviour
{
    public bool facingRight = false;

    Animator anime;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6.5f;
    public static GameObject player;
    public GameObject Death;
    public GameObject actualDeath;
    public GameObject deathText;
    public GameObject iceBook;
    public GameObject fireBook;
    public GameObject speedBook;
    public GameObject earthBook;
    public bool fireBookHeld;
    public bool iceBookHeld;
    public bool speedBookHeld;
    public bool earthBookHeld;
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    public GameObject ice1;
    public GameObject ice2;
    public GameObject ice3;
    public GameObject speed1;
    public GameObject speed2;
    public GameObject speed3;
    public GameObject earth1;
    public GameObject earth2;
    public GameObject earth3;
    public GameObject speedText;
    public GameObject iceText;
    public GameObject fireballText;
    public GameObject earthText;
    public GameObject healthBar;
    public GameObject scoreText;

    public int scoreInt;
    public bool iceUnlocked;
    public bool earthUnlocked;

    public float moveX;

    float formerPosition = 0;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    public Scene currentScene;
    public Rigidbody2D rb;
    string sceneName;
    public static int sceneInt;
    

    public int bookHeldInt = 0;
    Vector2 directionalInput;
    public string savefile;
    HealthManager healthManager;
    public static int PlayerHealth=250;

    public ParticleSystem runDust;
    public ParticleSystem speedDust;
    public ParticleSystem speedEffect;
    public ParticleSystem iceEffect;
    public ParticleSystem fireEffect;

    void Start()
    {
         currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
        if (sceneName == "Level1")
        {
            sceneInt = 1;
        }

        anime = GetComponent<Animator>();
        fireBookHeld = true;
        Physics2D.IgnoreLayerCollision(8, 9);
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 10)
        {
            if (collision.gameObject.layer == 10 && collision.gameObject.transform.position.x > player.transform.position.x)
            {
                velocity.x += -40;
            }
            if (collision.gameObject.layer == 10 && collision.gameObject.transform.position.x <= player.transform.position.x)
            {
                velocity.x += 40;
            }
            StartCoroutine(WaitEnemy(.75f));
        }
    }


    void Update()
    {
        string scoreIntString = scoreInt.ToString();
        scoreText.GetComponent<Text>().text = scoreIntString;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        anime.SetFloat("Speed", Math.Abs(h));

       

        if (gameObject.transform.position.y <= -100)
        {
            Dead();
        }
        if (gameObject.GetComponent<HealthManager>().health <= 0)
        {
            Dead();
        }

        PlayerMoves();

        CalculateVelocity();


        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }

    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnSpaceJumpInputDown()
    {
        if (controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
            CreateRunDust();
        }
    }
    public void OnWJumpInputDown()
    {
        if (controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
            CreateRunDust();
        }
    }


    public void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;


    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {

            StartCoroutine(WaitEnemy(.75f));
        }
        if (collision.gameObject.tag == "SlothBoss")
        {
            StartCoroutine(WaitEnemy(.75f));
        }
    }
    
    public void OnJumpInputUp()
    {
       
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
            CreateRunDust();
        }
        
    }
    
    IEnumerator WaitQuit(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.Quit();
    }
    IEnumerator WaitEnemy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    public void Dead()
    {
        fire1.SetActive(false);
        fire2.SetActive(false);
        fire3.SetActive(false);
        ice1.SetActive(false);
        ice2.SetActive(false);
        ice3.SetActive(false);
        healthBar.SetActive(false);
        SceneManager.LoadScene("GameOver");
    }
    public void SavePlayer()
    {
        string path = "SaveFile/Save.txt";
        player = GameObject.Find("Player");
        var playerComp = player.GetComponent<Player>();
        String saveNumber;
        if (PlayerHealth < 10)
        {
            saveNumber = "00" + PlayerHealth.ToString();
        }
        else if (PlayerHealth < 100)
        {
            saveNumber = "0" + PlayerHealth.ToString();
        }
        else { saveNumber = PlayerHealth.ToString(); }
        saveNumber += sceneInt.ToString();
        if (playerComp.iceUnlocked)
        {
            saveNumber += "1";
        }
        else saveNumber += "0";
        if (playerComp.earthUnlocked)
        {
            saveNumber += "1";
        }
        else saveNumber += "0";

        string createText = saveNumber+ Environment.NewLine;
        File.WriteAllText(path, createText);
    }



    public void Load()
    {
        string path = "SaveFile/Save.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        savefile = reader.ReadToEnd();
        char[] b = savefile.ToCharArray();
        PlayerHealth = b[0] * 100 + b[1] * 10 + b[2];
        sceneInt = b[3] - 48;
        if (b[4] == 1)
        {
            iceUnlocked = true;
        }
        else { iceUnlocked = false; }
        if (b[5] == 1)
        {
            earthUnlocked = true;
        }
        else { earthUnlocked = false; }
        reader.Close();
    }
    void PlayerMoves()
    {
        //Player Direction
        player = GameObject.Find("Player");
        if (player.transform.position.x < formerPosition && !facingRight)
        {
            CreateRunDust();
            FlipPlayer();
        }
        else if (player.transform.position.x > formerPosition && facingRight)
        {
            CreateRunDust();
            FlipPlayer();
        }
        formerPosition = player.transform.position.x;


        //Physics
        void FlipPlayer()
        {
            facingRight = !facingRight;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

    } 
    //Functions for particle creation for books and movement
    public void CreateRunDust()
    {
        runDust.Play();
    }

    public void CreateSpeedDust()
    {
        speedDust.Play();
    }

    public void CreateSpeedEffect()
    {
        speedEffect.Play();
    }

    public void CreateIceEffect()
    {
        iceEffect.Play();
    }

    public void CreateFireEffect()
    {
        fireEffect.Play();
    }

    //Functions for particle deletion for books and movements
    public void DisableRunDust()
    {
        runDust.Stop();
    }
    public void DisableSpeedDust()
    {
        speedDust.Stop();
    }
    public void DisableSpeedEffect()
    {
        speedEffect.Stop();
    }
    public void DisableIceEffect()
    {
        iceEffect.Stop();
    }
    public void DisableFireEffect()
    {
        fireEffect.Stop();
    }
}
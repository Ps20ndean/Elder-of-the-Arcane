using UnityEngine;
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
        // sets the sceneint and writes to the eventlog and also imports the players score
        if (sceneName == "Level1")
        {
            sceneInt = 1;
            string path = "Logs/EventLog.txt";
            File.AppendAllText(path, " Entered Level 1");
            scoreInt = 0;
            LoadScore();
            
        }
        if (sceneName == "Level2")
        {
            sceneInt = 2;
            iceUnlocked = true;
            string path = "Logs/EventLog.txt";
            File.AppendAllText(path, " Entered Level 2");
            scoreInt = 0;
            LoadScore();

        }
        if (sceneName == "Level3")
        {
            sceneInt = 3;
            iceUnlocked = true;
            string path = "Logs/EventLog.txt";
            File.AppendAllText(path, " Entered Level 3");
            scoreInt = 0;
            LoadScore();

        }
        //initialization
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
        //calculates player knockback
        if (collision.gameObject.layer == 10)
        {
            if (collision.gameObject.transform.position.x > player.transform.position.x)
            {
                velocity.x += -40;
            }
            if (collision.gameObject.transform.position.x <= player.transform.position.x)
            {
                velocity.x += 40;
            }
            StartCoroutine(WaitEnemy(.75f));
        }
    }


    void Update()
    {
        //updates the score text
        string scoreIntString = scoreInt.ToString();
        scoreText.GetComponent<Text>().text = scoreIntString;
        //runs the player movement animations
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        anime.SetFloat("Speed", Math.Abs(h));

       
        //checks if player fell off of the map
        if (gameObject.transform.position.y <= -100)
        {
            PlayerHealth = 0;
            Dead();
        }
       
        //checks player movement
        PlayerMoves();
        //checks player velocity
        CalculateVelocity();

        //controls aerial movement
        controller.Move(velocity * Time.deltaTime, directionalInput);
        //handles slope movement
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
        if (controller.collisions.below) // determines if you can jump
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
    public void OnWJumpInputDown()//alterntive w to jump
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
            //handles invincibility
            StartCoroutine(WaitEnemy(.75f));
        }
    }
    
    public void OnJumpInputUp()
    {
       //handles jumping
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
            CreateRunDust();
        }
        
    }
    
    IEnumerator WaitQuit(float seconds)
    {
        //delayed quit
        yield return new WaitForSeconds(seconds);
        Application.Quit();
    }
    IEnumerator WaitEnemy(float seconds)
    { 
        //ienumerator for enemy delays
        yield return new WaitForSeconds(seconds);
    }
    public void Dead()
    { 
        //handles player death
        if (PlayerHealth <= 0) {
            PlayerHealth = 250;
            SavePlayer();
            string path = "Logs/EventLog.txt";
            File.AppendAllText(path, " Player Died");
            SceneManager.LoadScene("GameOver");
            fire1.SetActive(false);
            fire2.SetActive(false);
            fire3.SetActive(false);
            ice1.SetActive(false);
            ice2.SetActive(false);
            ice3.SetActive(false);
            healthBar.SetActive(false);
        }
    }
    public void SavePlayer()
    {
        //handles player saving
        Scene currentScene = SceneManager.GetActiveScene();
        if ((currentScene.name != "Tutorial")|| (currentScene.name != "Menu"))
        {
            string path = "Logs/EventLog.txt";
            File.AppendAllText(path, " Player Saved");
            path = "SaveFile/Save.txt";
            player = GameObject.Find("Player");
            var playerComp = player.GetComponent<Player>();
            String saveNumber;
            //ensures that the player health will be saved as 3 digits
            if (PlayerHealth < 0)
            {
                PlayerHealth = 250 + PlayerHealth;
            }
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
            //ensures that the player score will be saved as 5 digits
            if (scoreInt < 10)
            {
                saveNumber += "0000" + scoreInt;
            }
            else if (scoreInt < 100)
            {
                saveNumber += "000" + scoreInt;
            }
            else if (scoreInt < 1000)
            {
                saveNumber += "00" + scoreInt;
            }
            else if (scoreInt < 10000)
            {
                saveNumber += "0" + scoreInt;
            }
            else
            {
                saveNumber += scoreInt;
            }

            //writes the save data to the save.txt text file
            string createText = saveNumber + Environment.NewLine;
            File.WriteAllText(path, createText);
        }
    }



    public void Load()
    {
        //loads the player information
        string path = "SaveFile/Save.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        savefile = reader.ReadToEnd();
        reader.Close();
        int myInt;
        if (savefile != null&&savefile.Length>8&& int.TryParse(savefile, out myInt))
        { 
        PlayerHealth = Convert.ToInt32(savefile.Substring(0, 3));
            LoadScore();
            //loads the correct scene based on what was save to the save file
            SceneManager.LoadScene(Convert.ToInt32(savefile.Substring(3, 1)) + 2);
           
          
    }   else
        {
            //error handling incase the save file does not contain the right amount of characters
            PlayerHealth = 250;
            sceneInt = 1;
            scoreInt = 0;
        }


    }
    public void LoadScore()
    {
        //loads only the score for optimization
        string path = "SaveFile/Save.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        savefile = reader.ReadToEnd();
        reader.Close();
        int myInt;
        if (savefile != null && savefile.Length > 8 && int.TryParse(savefile, out myInt))
        {
            scoreInt = (Convert.ToInt32(savefile.Substring(4, 5)));
        }
    }
    void PlayerMoves()
    {
        //Player Direction and which direction they face
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


        //handles which direction the player is facing
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
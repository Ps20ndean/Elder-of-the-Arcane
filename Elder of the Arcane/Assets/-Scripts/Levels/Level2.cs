using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class Level2 : MonoBehaviour
{
    private GameObject player;
    private static Player playerComp;
    public GameObject tavernText;
    private bool ableTo = false;

    private void Start()
    {
        // sets player to the gameobject player
        player = GameObject.Find("Player");
        playerComp = player.GetComponent<Player>();
    }

    private void Update()
    {
        // if theres not a king slime then check collisions
        if (GameObject.Find("King Slime") == null)
        {
            CheckCollision();
        }
        
    }

    private void CheckCollision()
    {
        if (ableTo) // if its able to do the following
        {
            // text on screen is set to true
            tavernText.SetActive(true);

            // if you press e then do the following
            if (Input.GetKeyDown(KeyCode.E))
            {
                // current scene is equal to the current scenes name
                Scene currentScene = SceneManager.GetActiveScene();
                if (currentScene.name == "Tutorial")
                {

                    // load menu if youre in tutorial when its pressed
                    SceneManager.LoadScene("Menu");
                }
                else if (currentScene.name == "Level1")
                {

                    // load level 2 if youre in level 1 when you press it
                    SceneManager.LoadScene("Level2");
                } else if (currentScene.name == "Level2")
                {
                    //if theres not a skeleton boss then portal becomes active
                    if (GameObject.Find("SkellyBoss") == null)
                    {
                        // load level 3 if youre in level 2 when you press it
                        SceneManager.LoadScene("Level3");
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // if theres a trigger collision between the player then able to is set to true
            ableTo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // if the player leaves the area, sets able to to false and de activates the text on screen
            ableTo = false;
            tavernText.SetActive(false);
        }
    }
}

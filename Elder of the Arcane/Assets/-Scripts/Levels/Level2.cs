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
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (GameObject.Find("King Slime") == null)
        {
            CheckCollision();
        }
        
    }

    private void CheckCollision()
    {
        if (ableTo)
        {
            tavernText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Scene currentScene = SceneManager.GetActiveScene();
                if (currentScene.name == "Tutorial")
                {
                    SceneManager.LoadScene("Menu");
                }
                else if (currentScene.name == "Level1")
                {
                    SceneManager.LoadScene("Level2");
                } else if (currentScene.name == "Level2")
                {
                    SceneManager.LoadScene("Level3");
                }
                playerComp.SavePlayer();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ableTo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ableTo = false;
            tavernText.SetActive(false);
        }
    }
}

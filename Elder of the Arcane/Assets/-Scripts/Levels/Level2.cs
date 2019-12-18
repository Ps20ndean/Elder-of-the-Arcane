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
    private static Player playercomp;
    public GameObject tavernText;
    private bool ableTo = false;

    private void Update()
    {
        CheckCollision();
        
}

    private void CheckCollision()
    {
        if (ableTo)
        {
            tavernText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {

                SceneManager.LoadScene("Level2");
                playercomp.SavePlayer();
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

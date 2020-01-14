using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Taverndooroutside : MonoBehaviour
{
    public GameObject tavernText;
    private bool ableTo = false;

    private void Update()
    {
        // runs the check tavern method
        CheckTavern();
    }

    private void CheckTavern()
    {
        if (ableTo) // if able to then do the following
        {
            // on screen text is activated
            tavernText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) // if player presses e do the following
            {
                SceneManager.LoadScene("Tavern (Start)"); // loads the tavern scene
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // if theres a collision with the player then set able to to true
            ableTo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // if a collision with the player is true then set able to to false and de activate the on screen text
            ableTo = false;
            tavernText.SetActive(false);
        }
    }
}

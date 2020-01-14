using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Taverndoorinside : MonoBehaviour
{
    public GameObject tavernText;
    private bool ableTo = false;

    private void Update()
    {
        // runs checktavern method
        CheckTavern();
    }

    private void CheckTavern()
    {
        if (ableTo) // if able to is true then do the following
        {
            tavernText.SetActive(true); // tavern text is enabled
            if (Input.GetKeyDown(KeyCode.E)) // if the player pushed e do the following
            {
                SceneManager.LoadScene("Level1"); // loads level 1 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // if theres a collision between player then sets able to to true
            ableTo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // if the player leaves the trigger then able to is false and the tavern text is de activated
            ableTo = false;
            tavernText.SetActive(false);
        }
    }
}

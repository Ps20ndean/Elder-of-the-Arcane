using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chests : MonoBehaviour
{
    private GameObject player;
    public GameObject DontMessWithThisText;
    public int TimesCollected = 0;
    public bool canCollect;

    private void Start()
    {
        player = GameObject.Find("Player"); // finds the player and assigns it

    }

    private void Update()
    {
        checkChest(); // runs the check chest method
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canCollect = true; // sets bool so player can collect it
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        canCollect = false; // bool control
        DontMessWithThisText.SetActive(false); // disables the text
    }

    public void checkChest()
    {
        var playerComp = player.GetComponent<Player>();

        if (canCollect && TimesCollected < 1) // if the player hasn't collected a chest
        {
            DontMessWithThisText.SetActive(true); // enables the text

            if (Input.GetKeyDown(KeyCode.E) && TimesCollected < 1) // if can collect and presses e
            {
                TimesCollected += 1; // int control
                playerComp.scoreInt += 1000; // add 1000 score
            }
        }
        else if (TimesCollected >= 1)
        {
            DontMessWithThisText.SetActive(false);
        }
        else
        {
            // do nothing
        }
    }
}

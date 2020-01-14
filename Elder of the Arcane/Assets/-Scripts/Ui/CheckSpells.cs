using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSpells : MonoBehaviour
{
    private GameObject player;
    public GameObject iceInv;
    public GameObject iceBlocker;
    public GameObject iceRedBlocker;
    public GameObject earthInv;
    private void Start()
    {
        // finds the gameobject player
        player = GameObject.Find("Player");
    }
    public void Update()
    {
        // runs check ice method
        CheckIce();
    }

    public void CheckIce()
    {
        // playercomp = the player script attached to the player
        var playerComp = player.GetComponent<Player>();

        // ice button is the button of ice inside the inventory
        var iceButton = iceInv.GetComponent<Button>();

        // if the ice unlocked is true do the following
        if (playerComp.iceUnlocked == true)
        {
            // allows the player to click the ice button and use ice. Gets rid of the ui blockers
            iceButton.enabled = true;
            iceBlocker.GetComponent<Image>().enabled = false;
            iceRedBlocker.GetComponent<Image>().enabled = false;
        } else
        {
            // disables the ability for the player to click the ice button
            iceButton.enabled = false;
            iceBlocker.GetComponent<Image>().enabled = true;
            iceRedBlocker.GetComponent<Image>().enabled = true;
        }
    }
}

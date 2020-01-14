using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentSpell : MonoBehaviour {
    public Sprite fire;
    public Sprite ice;
    public Sprite sound;
    public Sprite earth;
    private GameObject player;
    void Start()
    {
        // player is equal to the gameobject player in the screen
        player = GameObject.Find("Player");
    }
    // Update is called once per frame void 
    private void Update() 
    {
        var playerComp = player.GetComponent<Player>();
        if (playerComp.fireBookHeld) // if the player is using fire
        {
            // sets sprite to fireball
            GetComponent<Image>().sprite = fire;
        }
        else if (playerComp.iceBookHeld) // if player is using ice
        {
            // sets sprite to ice
            GetComponent<Image>().sprite = ice;
        }
        else if (playerComp.speedBookHeld) // if player is using speed
        {
            // sets sprite to sound
            GetComponent<Image>().sprite = sound;
        }
        else if (playerComp.earthBookHeld) // if the player is using heal
        {
            // sets the sprite to healing
            GetComponent<Image>().sprite = earth;
        }
    }
}
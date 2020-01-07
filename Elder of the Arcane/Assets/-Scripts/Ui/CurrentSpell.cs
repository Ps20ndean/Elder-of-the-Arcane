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
        player = GameObject.Find("Player");
    }
    // Update is called once per frame void 
    private void Update() 
    {
        var playerComp = player.GetComponent<Player>();
        if (playerComp.fireBookHeld)
        {
            GetComponent<Image>().sprite = fire;
        }
        else if (playerComp.iceBookHeld)
        {
            GetComponent<Image>().sprite = ice;
        }
        else if (playerComp.speedBookHeld)
        {
            GetComponent<Image>().sprite = sound;
        }
        else if (playerComp.earthBookHeld)
        {
            GetComponent<Image>().sprite = earth;
        }
    }
}
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
    public GameObject earthBlocker;
    public GameObject earthRedBlocker;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void Update()
    {
        CheckEarth();
        CheckIce();
    }

    public void CheckIce()
    {
        var playerComp = player.GetComponent<Player>();
        var iceButton = iceInv.GetComponent<Button>();
        if (playerComp.iceUnlocked == true)
        {
            iceButton.enabled = true;
            iceBlocker.GetComponent<Image>().enabled = false;
            iceRedBlocker.GetComponent<Image>().enabled = false;
        } else
        {
            iceButton.enabled = false;
            iceBlocker.GetComponent<Image>().enabled = true;
            iceRedBlocker.GetComponent<Image>().enabled = true;
        }
    }

    public void CheckEarth()
    {
        var earthButton = earthInv.GetComponent<Button>();
        var playerComp = player.GetComponent<Player>();
        if (playerComp.earthUnlocked == true)
        {
            earthButton.enabled = true;
            earthBlocker.GetComponent<Image>().enabled = false;
            earthRedBlocker.GetComponent<Image>().enabled = false;
        }
        else
        {
            earthButton.enabled = false;
            earthBlocker.GetComponent<Image>().enabled = true;
            earthRedBlocker.GetComponent<Image>().enabled = true;
        }
    }
}

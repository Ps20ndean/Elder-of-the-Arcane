using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellButton : MonoBehaviour
{
    private GameObject player;
    private Player playerComp;

    public void Start()
    {
        player = GameObject.Find("Player");
        playerComp = player.GetComponent<Player>();
    }

    public void UseIce()
    {
        // sets all of the ice components to true and disables all other spell components
        playerComp.iceBookHeld = true;
        playerComp.fireBookHeld = false;
        playerComp.speedBookHeld = false;
        playerComp.earthBookHeld = false;

        playerComp.CreateIceEffect();
        playerComp.DisableFireEffect();
        playerComp.DisableRunDust();
        playerComp.DisableSpeedEffect();
    }
    public void UseFire()
    {
        // sets all of the fire components to true and disables all other spell components
        playerComp.iceBookHeld = false;
        playerComp.fireBookHeld = true;
        playerComp.speedBookHeld = false;
        playerComp.earthBookHeld = false;

        playerComp.DisableIceEffect();
        playerComp.CreateFireEffect();
        playerComp.DisableRunDust();
        playerComp.DisableSpeedEffect();
    }
    public void UseSpeed()
    {
        // sets all of the speed components to true and disables all other spell components
        playerComp.iceBookHeld = false;
        playerComp.fireBookHeld = false;
        playerComp.speedBookHeld = true;
        playerComp.earthBookHeld = false;

        playerComp.DisableIceEffect();
        playerComp.DisableFireEffect();
        playerComp.DisableRunDust();
        playerComp.CreateSpeedEffect();
    }

    public void UseEarth()
    {
        // sets all of the healing components to true and disables all other spell components
        playerComp.iceBookHeld = false;
        playerComp.fireBookHeld = false;
        playerComp.speedBookHeld = false;
        playerComp.earthBookHeld = true;

        playerComp.DisableIceEffect();
        playerComp.DisableFireEffect();
        playerComp.DisableRunDust();
        playerComp.DisableSpeedEffect();
    }

    public void UseNothing()
    {
        // sets everything to false
        playerComp.iceBookHeld = false;
        playerComp.fireBookHeld = false;
        playerComp.speedBookHeld = false;
        playerComp.earthBookHeld = false;

        playerComp.DisableIceEffect();
        playerComp.DisableFireEffect();
        playerComp.DisableRunDust();
        playerComp.DisableSpeedEffect();
    }
}

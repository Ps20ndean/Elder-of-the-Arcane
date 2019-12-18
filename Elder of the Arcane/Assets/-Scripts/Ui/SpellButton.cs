using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellButton : MonoBehaviour
{
    private GameObject player;
    public Player playerComp;

    public void Start()
    {
        player = GameObject.Find("Player");
        playerComp = player.GetComponent<Player>();
    }

    public void UseIce()
    {
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

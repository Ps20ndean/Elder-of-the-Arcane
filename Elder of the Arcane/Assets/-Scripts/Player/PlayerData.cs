using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public int health;
    public string[] spells;
    public float[] position;

    public PlayerData (Player Player)
    {
        level = Player.sceneInt;
        health = Player.GetComponent<HealthManager>().GetHealth();
        position[0] = Player.transform.position.x;
        position[1] = Player.transform.position.y;
    }
    
}

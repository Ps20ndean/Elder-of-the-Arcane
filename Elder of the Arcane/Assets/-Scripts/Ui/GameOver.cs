using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void LoadGameDeath()
    {
        // spawns a new player
        Player player = new Player();
        
        // runs the players load function
        player.Load();

        // loads the first scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Player.sceneInt + 1);
    }
    public void ReturnToMenu()
    {
        // returns the user to the menu
        SceneManager.LoadScene(0);
    }
}


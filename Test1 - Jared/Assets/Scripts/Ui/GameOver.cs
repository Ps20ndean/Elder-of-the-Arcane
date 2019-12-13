using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void LoadGameDeath()
    {
        Player player = new Player();
        player.Load();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Player.sceneInt + 1);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}


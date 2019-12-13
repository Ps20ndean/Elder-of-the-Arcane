using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMen : MonoBehaviour
{
    //Starts new game
    public void NewGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Loads save
    public void LoadGame()
    {
        Player player = new Player();
        player.Load();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Player.sceneInt+1);
    }
    //Exits the game
    public void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMen : MonoBehaviour
{
    //Starts new game
    public void NewGame ()
    {
        File.WriteAllText("SaveFile/Save.txt", "250100000");
        SceneManager.LoadScene("Tavern (Start)");
    }
    //Loads save
    public void LoadGame()
    {
        Player player = new Player();
        player.Load();
    }
    //Exits the game
    public void ExitGame()
    {
        // exits the game
        Application.Quit();
    }
    public void LoadTutorial()
    {
        // loads tutorial
        SceneManager.LoadScene("Tutorial");
    }
    public void SaveGame()
    {
        Player player = new Player();
        player.SavePlayer();
    }
}

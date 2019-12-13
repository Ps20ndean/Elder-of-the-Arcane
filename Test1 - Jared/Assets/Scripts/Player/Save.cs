using System;
using System.IO;
using System.Text;
using UnityEngine;


public class SaveSystem : MonoBehaviour
{
    private GameObject player;
    private static Player playercomp;
    public void Start()
    {
        player = GameObject.Find("Player");
        playercomp = player.GetComponent<Player>();
    }

    public static void SavePlayer()
    {
    string path = "SaveFile/Save.txt";
        
        // This text is added only once to the file.
        

          }
    public void LoadPlayer()
    {
        /* Open the file to read from.
        string readText = File.ReadAllText(path);
        Console.WriteLine(readText); */
    }
}

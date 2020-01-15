using UnityEngine;
using System.Collections;


public class PlayerInput : MonoBehaviour {

	Player player;
    public GameObject inventory;
    public GameObject hearts;
    private GameObject players;

    private bool invOn = false;
    void Start () {
		player = GetComponent<Player>();
	}

	void Update () {

        var players = GameObject.Find("Player");
        //detects user directional input
        if (Time.timeScale != 0)
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);
        }
        //runs jump command used to detect how high jump should go
		if (Input.GetKeyDown (KeyCode.Space)) {
			player.OnSpaceJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}//runs jump command with set height for alternitive jump option
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.OnWJumpInputDown();
        }//closes game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //opens and closes inventory
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //pauses or resumes game based on if the inventory is open
            if (invOn)
            {
                invOn = false;
                Time.timeScale = 1;
            }
            else if (!invOn)
            {
                invOn = true;
                Time.timeScale = 0;
            }
        }
        if (invOn)
        {
            inventory.SetActive(true);
            
        }
        else if (!invOn)
        {
            inventory.SetActive(false);
        }
    }
}

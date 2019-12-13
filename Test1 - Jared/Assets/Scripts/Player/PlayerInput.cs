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
        if (Input.GetKeyDown(KeyCode.F))
        {
            players.GetComponent<HealthManager>().healthMax += 100000;
            players.GetComponent<HealthManager>().health += 1000000;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Testing Only Delete me
            players.GetComponent<HealthManager>().healthMax -= 10;
            players.GetComponent<HealthManager>().health -= 10;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Testing Only Delete me
            player.GetComponent<Player>().moveSpeed = 20;
            player.CreateRunDust();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.GetComponent<Player>().moveSpeed = 6.5f;
        }

        Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) {
			player.OnSpaceJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.OnWJumpInputDown ();
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            Mail.SendMail();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
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

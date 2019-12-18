using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Taverndooroutside : MonoBehaviour
{
    public GameObject tavernText;
    private bool ableTo = false;

    private void Update()
    {
        CheckTavern();
    }

    private void CheckTavern()
    {
        if (ableTo)
        {
            tavernText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Tavern (Start)");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ableTo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ableTo = false;
            tavernText.SetActive(false);
        }
    }
}

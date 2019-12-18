using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Taverndoorinside : MonoBehaviour
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
                SceneManager.LoadScene("Level1");
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

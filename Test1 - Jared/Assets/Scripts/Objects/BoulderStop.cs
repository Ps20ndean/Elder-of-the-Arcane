using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderStop : Boulder
{
    public Animator anime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boulder")
        {
            anime.SetBool("movement", false);
            movement = false;
        }
        }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boulder")
        {
            anime.SetBool("movement", false);
           movement = false;
        }
    }
}

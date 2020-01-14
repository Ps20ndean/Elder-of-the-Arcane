using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderStop : Boulder
{
    public Animator anime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if theres a collision with the boulder sets movement to false and turns off the animation
        if (collision.gameObject.tag == "Boulder")
        {
            anime.SetBool("movement", false);
            movement = false;
        }
        }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // if the boulder stays in the same spot then do the following
        if (collision.gameObject.tag == "Boulder")
        {
            //turns animations off and turns off movement
            anime.SetBool("movement", false);
           movement = false;
        }
    }
}

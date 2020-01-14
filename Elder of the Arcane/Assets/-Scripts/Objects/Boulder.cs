using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Boulder : MonoBehaviour
{
    public Transform target;
    public float dist;
    public bool inDist;
    public float stoppingDistance = 0f;
    public bool movement = false;
    public float movementSpeed = .75f;
    private Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        // sets the target to the actual players transform component
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // anime is set to the animator component
        anime = GetComponent<Animator>();
        // it starts off without movement
        movement = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) // if the player isnt dead then do the following
        {
            dist = Math.Abs(Vector3.Distance(target.position, transform.position)); // dist is equal to the distance between the player and boulder
            if (dist <= stoppingDistance) // if dist is less than or equal to the stopping dist then do the following
            {
                // in dist is set to true
                inDist = true;
            }
            else if (dist > stoppingDistance) // if dist is greater than stopping dist do the following
            {
                // in dist is set to false
                inDist = false;
            }

            if (movement && inDist)
            {
                // if it can move and in distance of player then move towards the player
                transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // if a collision with a player then set movement to true, animation of movement is set to true too
            movement = true;
            anime.SetBool("movement", true);
        }
        if (collision.gameObject.tag == "BoulderStop")
        {
            // if it hits the stopping check then sets movement to false and turns the animation off
            movement = false;
            anime.SetBool("movement", false);
        }
        }
}

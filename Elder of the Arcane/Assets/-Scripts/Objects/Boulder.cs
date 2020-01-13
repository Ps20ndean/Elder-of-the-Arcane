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
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anime = GetComponent<Animator>();
        movement = false;   
    }

    // Update is called once per frame
    void Update()
    {
        dist = Math.Abs(Vector3.Distance(target.position, transform.position));
        if (dist <= stoppingDistance)
        {
            inDist = true;
        }
        else if (dist > stoppingDistance)
        {
            inDist = false;
        }

        if (movement && inDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movement = true;
            anime.SetBool("movement", true);
        }
        if (collision.gameObject.tag == "BoulderStop")
        {
            movement = false;
            anime.SetBool("movement", false);
        }
        }
}

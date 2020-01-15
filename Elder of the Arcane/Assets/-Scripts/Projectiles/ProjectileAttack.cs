using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileAttack : MonoBehaviour
{
    // makes and sets variables
    public GameObject fireball;
    public GameObject ice;
    public GameObject heart;
    public GameObject speed;
    public Player player;
    GameObject b;
    public bool canAttack = true;
    public int fireChargeAmounts = Mathf.Max(3);
    public int iceChargeAmounts = Mathf.Max(3);
    public int speedChargeAmounts = Mathf.Max(3);
    public int earthChargeAmounts = Mathf.Max(3);
    public GameObject inventory;

    public AudioClip fireballSound;
    public AudioClip iceSound;

    public AudioSource iceSource;
    public AudioSource fireballSource;

    public bool Charging = false;

    public int varFacingRight;

    public GameObject healText;

    public void Start()
    {
        // ignores the collisions between the player and the projectiles
        Physics2D.IgnoreLayerCollision(8, 11);
    }
    public void Update()
    {
        varFacingRight = 1; // default value of facing right

        if (player.facingRight == false)
        {
            // if the player is looking left then set the value to -1
            varFacingRight = -1;

        }
        checkBookHeld(); // checks which book is being held by the player
        if (canAttack && fireChargeAmounts >= 1 && player.fireBookHeld)
        { 
            // if the player can attack, has more than one fire charge, and is holding the fire book and then clicks k or l do the following
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetKeyDown(KeyCode.L)))
            {
                // remove a single charge from the fireball charges
                fireChargeAmounts -= 1;

                // shoots a fireball in front of them
                    ShootFireball();
            }
        }
        if (canAttack && iceChargeAmounts >= 1 && player.iceBookHeld)
        {
            // if the player can attack, has more than one ice charge, and is holding the ice book and then clicks k or l do the following
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetKeyDown(KeyCode.L)))
            {
                // remove a single charge from the ice charges
                iceChargeAmounts -= 1;

                // runs the shoot ice method, which spawns ice spikes
                ShootIce();
            }
        }
        if (canAttack && speedChargeAmounts >= 1 && player.speedBookHeld)
        {
            // if the player can attack, has more than one speed charge, and is holding the speed book and then clicks k or l do the following
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetKeyDown(KeyCode.L)))
            {
                // removes a single charge from the speed charges
                speedChargeAmounts -= 1;

                // runs the shoot speed method, which allows the player to move faster for a short period of time
                ShootSpeed();
            }
        }
        if (canAttack && earthChargeAmounts >= 1 && player.earthBookHeld)
        {
            // if the player can attack, has more than one heal charge, and is holding the healing book and then clicks k or l do the following
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetKeyDown(KeyCode.L)))
            {
                // removes a single healing charge
                earthChargeAmounts -= 1;

                //runs shoot heal method, which heals the player for 100
                ShootHeal();
            }
        }
    }

    void ShootSpeed()
    {
        GameObject bspeed = (GameObject)(Instantiate(speed, transform.position, Quaternion.identity)); // spawns a clone of the speed prefab

        bspeed.transform.parent = player.transform; // clones parent is equal to the players position

        bspeed.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; // freezes the clones axises to match the players

        // sets the movement speed to 16, default being 6.5
        player.moveSpeed = 16;
        
        // changes speed back to normal after 1.5 seconds
        StartCoroutine(SpeedChange());

        // starts recharging the speed charges
        StartCoroutine(SpeedRecharge());

        // destroys the gameobject surrounding the player after 1.5 seconds
        Destroy(bspeed, 1.5f);
    }

    void ShootFireball()
    {
        // spawns a clone of a fireball in front of the player
        GameObject bfire = (GameObject)(Instantiate(fireball, transform.position + transform.up * .45f + transform.right * varFacingRight * -2f, Quaternion.identity));

        // pushes the fireball fast in front of it
        bfire.GetComponent<Rigidbody2D>().AddForce(transform.right * varFacingRight * -1000);
        
        // plays the fireball cast sound
        fireballSource.Play();

        // starts recharging the fireball
        StartCoroutine(RechargeFireball());

        if (varFacingRight == 1)
        {
            //rotates the fireball based on direction of player
            bfire.transform.Rotate(0, 0, -90f);
        }
        else if (varFacingRight == -1)
        {
            //rotates the fireball based on direction of player
            bfire.transform.Rotate(0, 0, 90f);
        }
        // destroys the clone after 2 second
        Destroy(bfire, 2f);
    }

    public IEnumerator TextWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        healText.SetActive(false);
        healText.GetComponent<Text>().text = " ";
    }
    void ShootIce()
    {
        // spawns three ice spikes in front of the player and above the player
        GameObject bice = (GameObject)(Instantiate(ice, transform.position + transform.up * 3f + transform.right * varFacingRight * -3f, Quaternion.identity));
        GameObject bice2 = (GameObject)(Instantiate(ice, transform.position + transform.up * 3f + transform.right * varFacingRight * -3.5f, Quaternion.identity));
        GameObject bice3 = (GameObject)(Instantiate(ice, transform.position + transform.up * 3f + transform.right * varFacingRight * -4f, Quaternion.identity));

        // makes the 3 ice spikes fall down to the earth
        bice.GetComponent<Rigidbody2D>().AddForce(transform.up * -1);
        bice2.GetComponent<Rigidbody2D>().AddForce(transform.up * -1);
        bice3.GetComponent<Rigidbody2D>().AddForce(transform.up * -1);
        // plays the ice audio
        iceSource.Play();
        // starts recharging the ice spell
        StartCoroutine(RechargeIce());


        // destroys the clones after 2 seconds
        Destroy(bice, 2f);
        Destroy(bice2, 2f);
        Destroy(bice3, 2f);
    }

    void ShootHeal()
    {
        var playercomp = player.GetComponent<HealthManager>();

        // spawns a clone of heart above the player
        GameObject bheart = (GameObject)(Instantiate(heart, transform.position + transform.up * 3f, Quaternion.identity));

        // disables the boxcollider component upon spawning
        bheart.GetComponent<BoxCollider2D>().enabled = false; // disables the collider of the spawned heart

        healText.SetActive(true); // enables text

        healText.GetComponent<Text>().text = "Healed by 100"; // shows on screen that youve been healed by 100

        StartCoroutine(TextWait(2f)); // waits 2 seconds and then resets the text and disables it

        playercomp.Heal(100); // heals the player by 100 hp

        Destroy(bheart, 2f); // destroys the cloned heart after 2 seconds
    }
    IEnumerator RechargeFireball()
    {

        while (fireChargeAmounts == 0 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(.75f);
            fireChargeAmounts += 1;
            Charging = false;

        }
        while (fireChargeAmounts == 1 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(.75f);
            fireChargeAmounts += 1;
            Charging = false;

        }
        while (fireChargeAmounts == 2 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(.75f);
            fireChargeAmounts += 1;
            Charging = false;
        }

        if (fireChargeAmounts == 1 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(.75f);
            fireChargeAmounts += 1;
            Charging = false;
        }
        if (fireChargeAmounts == 2 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(.75f);
            fireChargeAmounts += 1;
            Charging = false;
        }

        if (fireChargeAmounts > 4)
        {
            fireChargeAmounts = 3;
        }

    } // controls the recharge rate of the fire spell, if it goes over 4 charges then resets it to 3
    IEnumerator RechargeIce()
    {

        while (iceChargeAmounts == 0 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(1f);
            iceChargeAmounts += 1;
            Charging = false;

        }
        while (iceChargeAmounts == 1 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(1f);
            iceChargeAmounts += 1;
            Charging = false;

        }
        while (iceChargeAmounts == 2 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(1f);
            iceChargeAmounts += 1;
            Charging = false;
        }

        if (iceChargeAmounts == 1 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(1f);
            iceChargeAmounts += 1;
            Charging = false;
        }
        if (iceChargeAmounts == 2 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(1f);
            iceChargeAmounts += 1;
            Charging = false;
        }

        if (iceChargeAmounts > 4)
        {
            iceChargeAmounts = 3;
        }

    } // controls the recharge rate of the ice spell, if it goes over 4 charges then resets it to 3
    IEnumerator SpeedRecharge()
    {
        while (speedChargeAmounts == 0 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(7f);
            speedChargeAmounts += 1;
            Charging = false;

        }
        while (speedChargeAmounts == 1 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(7f);
            speedChargeAmounts += 1;
            Charging = false;

        }
        while (speedChargeAmounts == 2 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(7f);
            speedChargeAmounts += 1;
            Charging = false;
        }

        if (speedChargeAmounts == 1 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(7f);
            speedChargeAmounts += 1;
            Charging = false;
        }
        if (speedChargeAmounts == 2 && !Charging)
        {
            Charging = true;
            yield return new WaitForSeconds(7f);
            speedChargeAmounts += 1;
            Charging = false;
        }

        if (speedChargeAmounts > 4)
        {
            speedChargeAmounts = 3;
        }

    } // controls the recharge rate of the speed spell, if it goes over 4 charges then resets it to 3
    IEnumerator SpeedChange()
    {
        // waits 1.5 seconds and then resets speed to the default value
        yield return new WaitForSeconds(1.5f);
        player.moveSpeed = 6.5f;
    }
    void checkBookHeld()
    {
        if (fireChargeAmounts == 0 && player.fireBookHeld)
        {
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fireballText.SetActive(true);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(true);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = true;
            player.iceBookHeld = false;
            player.speedBookHeld = false;
            canAttack = false;
        }
        else if (fireChargeAmounts == 1 && player.fireBookHeld)
        {
            player.fire1.SetActive(true);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fireballText.SetActive(true);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(true);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = true;
            player.iceBookHeld = false;
            player.speedBookHeld = false;
            canAttack = true;
        }
        else if (fireChargeAmounts == 2 && player.fireBookHeld)
        {
            player.fire1.SetActive(true);
            player.fire2.SetActive(true);
            player.fire3.SetActive(false);
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fireballText.SetActive(true);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(true);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = true;
            player.iceBookHeld = false;
            player.speedBookHeld = false;
            canAttack = true;
        }
        else if (fireChargeAmounts == 3 && player.fireBookHeld)
        {
            player.fire1.SetActive(true);
            player.fire2.SetActive(true);
            player.fire3.SetActive(true);
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fireballText.SetActive(true);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(true);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = true;
            player.iceBookHeld = false;
            player.speedBookHeld = false;
            canAttack = true;
        }
        if (fireChargeAmounts >= 4)
        {
            fireChargeAmounts = 3;
        }
        if (iceChargeAmounts == 0 && player.iceBookHeld)
        {
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(true);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(true);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = false;
            player.iceBookHeld = true;
            player.speedBookHeld = false;
            canAttack = false;
        }
        else if (iceChargeAmounts == 1 && player.iceBookHeld)
        {

            player.ice1.SetActive(true);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(true);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(true);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = false;
            player.iceBookHeld = true;
            player.speedBookHeld = false;
            canAttack = true;
        }
        else if (iceChargeAmounts == 2 && player.iceBookHeld)
        {
            player.ice1.SetActive(true);
            player.ice2.SetActive(true);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(true);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(true);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = false;
            player.iceBookHeld = true;
            player.speedBookHeld = false;
            canAttack = true;
        }
        else if (iceChargeAmounts == 3 && player.iceBookHeld)
        {
            player.ice1.SetActive(true);
            player.ice2.SetActive(true);
            player.ice3.SetActive(true);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(true);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(true);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = false;
            player.iceBookHeld = true;
            player.speedBookHeld = false;
            canAttack = true;
        }
        if (iceChargeAmounts >= 4)
        {
            iceChargeAmounts = 3;
        }
        if (speedChargeAmounts == 0 && player.speedBookHeld)
        {
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(true);
            player.speedText.SetActive(true);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = false;
            player.iceBookHeld = false;
            player.speedBookHeld = true;
            canAttack = false;
        }
        else if (speedChargeAmounts == 1 && player.speedBookHeld)
        {

            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(false);
            player.speed1.SetActive(true);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(true);
            player.speedText.SetActive(true);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = false;
            player.iceBookHeld = false;
            player.speedBookHeld = true;
            canAttack = true;
        }
        else if (speedChargeAmounts == 2 && player.speedBookHeld)
        {
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(false);
            player.speed1.SetActive(true);
            player.speed2.SetActive(true);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(true);
            player.speedText.SetActive(true);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = false;
            player.iceBookHeld = false;
            player.speedBookHeld = true;
            canAttack = true;
        }
        else if (speedChargeAmounts == 3 && player.speedBookHeld)
        {
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(false);
            player.speed1.SetActive(true);
            player.speed2.SetActive(true);
            player.speed3.SetActive(true);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(true);
            player.speedText.SetActive(true);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(false);
            player.earthBook.SetActive(false);
            player.earthBookHeld = false;
            player.fireBookHeld = false;
            player.iceBookHeld = false;
            player.speedBookHeld = true;
            canAttack = true;
        }
        if (speedChargeAmounts >= 4)
        {
            speedChargeAmounts = 3;
        }
        if (earthChargeAmounts == 3 && player.earthBookHeld)
        {
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(true);
            player.earth2.SetActive(true);
            player.earth3.SetActive(true);
            player.earthText.SetActive(true);
            player.earthBook.SetActive(true);
            player.earthBookHeld = true;
            player.fireBookHeld = false;
            player.iceBookHeld = false;
            player.speedBookHeld = false;
            canAttack = true;
        }
        else if (earthChargeAmounts == 2 && player.earthBookHeld)
        {
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(true);
            player.earth2.SetActive(true);
            player.earth3.SetActive(false);
            player.earthText.SetActive(true);
            player.earthBook.SetActive(true);
            player.earthBookHeld = true;
            player.fireBookHeld = false;
            player.iceBookHeld = false;
            player.speedBookHeld = false;
            canAttack = true;
        }
        else if (earthChargeAmounts == 1 && player.earthBookHeld)
        {
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(true);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(true);
            player.earthBook.SetActive(true);
            player.earthBookHeld = true;
            player.fireBookHeld = false;
            player.iceBookHeld = false;
            player.speedBookHeld = false;
            canAttack = true;
        }
        else if (earthChargeAmounts == 0 && player.earthBookHeld)
        {
            player.ice1.SetActive(false);
            player.ice2.SetActive(false);
            player.ice3.SetActive(false);
            player.fire1.SetActive(false);
            player.fire2.SetActive(false);
            player.fire3.SetActive(false);
            player.fireballText.SetActive(false);
            player.iceText.SetActive(false);
            player.speed1.SetActive(false);
            player.speed2.SetActive(false);
            player.speed3.SetActive(false);
            player.fireBook.SetActive(false);
            player.iceBook.SetActive(false);
            player.speedBook.SetActive(false);
            player.speedText.SetActive(false);
            player.earth1.SetActive(false);
            player.earth2.SetActive(false);
            player.earth3.SetActive(false);
            player.earthText.SetActive(true);
            player.earthBook.SetActive(true);
            player.earthBookHeld = true;
            player.fireBookHeld = false;
            player.iceBookHeld = false;
            player.speedBookHeld = false;
            canAttack = false;
        }
        if (earthChargeAmounts >= 4)
        {
            earthChargeAmounts = 4;
        }
    } // sets the correct ui element based on what the player is holding and how many charges it has
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public GameObject fireball;
    public GameObject ice;
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
    public void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 11);
    }
    public void Update()
    {
        varFacingRight = 1;

        if (player.facingRight == false)
        {
            varFacingRight = -1;

        }
        checkBookHeld();
        if (canAttack && fireChargeAmounts >= 1 && player.fireBookHeld)
        {
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetKeyDown(KeyCode.L)))
            {
                fireChargeAmounts -= 1;
                    ShootFireball();
            }
        }
        if (canAttack && iceChargeAmounts >= 1 && player.iceBookHeld)
        {
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetKeyDown(KeyCode.L)))
            {
                iceChargeAmounts -= 1;
                ShootIce();
            }
        }
        if (canAttack && speedChargeAmounts >= 1 && player.speedBookHeld)
        {
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetKeyDown(KeyCode.L)))
            {
                speedChargeAmounts -= 1;
                ShootSpeed();
            }
        }
    }

    void ShootSpeed()
    {
        GameObject bspeed = (GameObject)(Instantiate(speed, transform.position, Quaternion.identity));
        bspeed.transform.parent = player.transform;
        bspeed.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        player.moveSpeed = 16;
        StartCoroutine(SpeedChange());
        StartCoroutine(SpeedRecharge());

        Destroy(bspeed, 1.5f);
    }

    void ShootFireball()
    {
        GameObject bfire = (GameObject)(Instantiate(fireball, transform.position + transform.up * .45f + transform.right * varFacingRight * -2f, Quaternion.identity));
        bfire.GetComponent<Rigidbody2D>().AddForce(transform.right * varFacingRight * -1000);
        fireballSource.Play();
        StartCoroutine(RechargeFireball());

        if (varFacingRight == 1)
        {
            bfire.transform.Rotate(0, 0, -90f);
        }
        else if (varFacingRight == -1)
        {
            bfire.transform.Rotate(0, 0, 90f);
        }
        Destroy(bfire, 2f);
    }

    void ShootIce()
    {
        GameObject bice = (GameObject)(Instantiate(ice, transform.position + transform.up * 3f + transform.right * varFacingRight * -3f, Quaternion.identity));
        GameObject bice2 = (GameObject)(Instantiate(ice, transform.position + transform.up * 3f + transform.right * varFacingRight * -3.5f, Quaternion.identity));
        GameObject bice3 = (GameObject)(Instantiate(ice, transform.position + transform.up * 3f + transform.right * varFacingRight * -4f, Quaternion.identity));
        bice.GetComponent<Rigidbody2D>().AddForce(transform.up * -1);
        bice2.GetComponent<Rigidbody2D>().AddForce(transform.up * -1);
        bice3.GetComponent<Rigidbody2D>().AddForce(transform.up * -1);

        StartCoroutine(RechargeIce());


        Destroy(bice, 2f);
        Destroy(bice2, 2f);
        Destroy(bice3, 2f);
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

    }
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

    }
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

    }
    IEnumerator SpeedChange()
    {
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
            canAttack = true;
        }
        if (speedChargeAmounts >= 4)
        {
            speedChargeAmounts = 3;
        }
        if (earthChargeAmounts == 3 && player.earthBookHeld == true)
        {

        }
    }
}


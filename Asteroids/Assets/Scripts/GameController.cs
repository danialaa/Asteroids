using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Ship player;
    public GameObject bullet;

    private List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        moveShip();
        directShip();
        shoot();
        moveBullets();
    }

    private void moveBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].GetComponent<BulletController>().shootBullet();
        }
    }

    private void directShip()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.directShip(1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.directShip(-1);
        }
    }

    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bullet, bullet.transform.position, Quaternion.identity);
            newBullet.transform.up = player.transform.up;
            bullets.Add(newBullet);
        }
    }

    void moveShip()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.moveShip();
        }
    }
}

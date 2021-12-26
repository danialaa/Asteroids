using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Ship player;
    public GameObject bulletObject;
    public GameObject asteroidObject;
    public int asteroidSpawnRate;
    public int lifetime;

    void Start()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }

        InvokeRepeating("generateAsteroid", 2f, asteroidSpawnRate);
    }

    void Update()
    {
        moveShip();
        directShip();
        shoot();
    }

    public GameObject generateAsteroid()
    {
        GameObject newAsteroid = Instantiate(asteroidObject, asteroidObject.transform.position, Quaternion.identity);

        return newAsteroid;
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

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bulletObject, player.transform.position, Quaternion.identity);
            newBullet.transform.up = player.transform.up;
        }
    }

    private void moveShip()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.moveShip();
        }
    }
}

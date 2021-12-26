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
    
    private List<GameObject> asteroids = new List<GameObject>();
    private List<GameObject> bullets = new List<GameObject>();

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
        moveBullets();
    }

    public GameObject generateAsteroid()
    {
        GameObject newAsteroid = Instantiate(asteroidObject, asteroidObject.transform.position, Quaternion.identity);
        asteroids.Add(newAsteroid);

        return newAsteroid;
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

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bulletObject, player.transform.position, Quaternion.identity);
            newBullet.transform.up = player.transform.up;
            bullets.Add(newBullet);
        }
    }

    private void moveShip()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.moveShip();
        }
    }

    public void removeAsteroid(GameObject asteroid)
    {
        asteroids.Remove(asteroid);
    }
}

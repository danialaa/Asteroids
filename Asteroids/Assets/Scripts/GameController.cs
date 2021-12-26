using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Ship player;
    public GameObject bulletObject;
    public GameObject asteroidObject;
    public GameObject scoreObject, livesObject;
    public int asteroidSpawnRate;
    public int lifetime;
    public int maxLives;
    public ParticleSystem explosion;
    public AudioSource shootSound, dieSound, hitSound;

    private int score;
    private int lives;

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
        lives = maxLives;
        livesObject.GetComponent<Text>().text = lives + "";
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
        newAsteroid.GetComponent<AsteroidController>().createRandomAsteroid();

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
            GameObject newBullet = Instantiate(bulletObject, player.transform.position, player.transform.rotation);
            newBullet.transform.up = player.transform.up;

            shootSound.PlayOneShot(shootSound.clip);
        }
    }

    private void moveShip()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.moveShip();

            for (int i = 0; i < player.transform.childCount; i++)
            {
                player.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < player.transform.childCount; i++)
            {
                player.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void takeDamage()
    {
        lives--;
        livesObject.GetComponent<Text>().text = lives + "";
        dieSound.PlayOneShot(dieSound.clip);

        if (lives < 0)
        {
            respawn(true);
        }
        else
        {
            respawn(false);
        }
    }

    public void destroyAsteroid(Vector3 position)
    {
        explosion.transform.position = position;
        explosion.Play();
    }

    private void respawn(bool isFullRestart)
    {
        if (isFullRestart)
        {
            lives = maxLives;
            score = 0;

            livesObject.GetComponent<Text>().text = lives + "";
            scoreObject.GetComponent<Text>().text = score + "";
            player.transform.eulerAngles = Vector3.zero;
        }

        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        for (int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i].gameObject);
        }
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i].gameObject);
        }
    }

    public void increaseScore(int amount)
    {
        score += amount;
        scoreObject.GetComponent<Text>().text = score + "";
        hitSound.PlayOneShot(hitSound.clip);
    }

    private IEnumerator pauseGame()
    {
        yield return new WaitForSeconds(3);
    }
}

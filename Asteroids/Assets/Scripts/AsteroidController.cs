using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public int speed;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;
    private AsteroidType type;
    private Vector2 direction;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        createRandomAsteroid();
        Destroy(gameObject, GameController.instance.lifetime);
    }

    private void createRandomAsteroid()
    {
        assignSize(Random.Range(1, 4));

        float screenHeight = Camera.main.orthographicSize * 2, screenWidth = screenHeight * Camera.main.aspect;
        Vector2 randomPosition = Random.insideUnitCircle.normalized;
        transform.position = new Vector3(randomPosition.x * screenWidth, randomPosition.y * screenHeight, 1);
        
        setDirection();
        rigidBody.AddForce(direction * speed);
    }

    private void setDirection()
    {
        int minValX = -1, maxValX = 1;
        int minValY = -1, maxValY = 1;

        if (transform.position.x > GameController.instance.player.transform.position.x)
        {
            maxValX = 0;
        }
        else
        {
            minValX = 0;
        }
        if (transform.position.y > GameController.instance.player.transform.position.y)
        {
            maxValY = 0;
        }
        else
        {
            minValY = 0;
        }

        double directionX = Random.Range(minValX, maxValX);
        double directionY = Random.Range(minValY, maxValY);
        direction = new Vector2((float)directionX, (float)directionY).normalized;
    }

    public void updateAsteroid(int type, Vector2 position, Vector2 direction)
    {
        assignSize(type);
        transform.position = position;
        this.direction = direction;
        rigidBody.AddForce(direction * speed);
    }

    private void assignSize(int size)
    {
        type = (AsteroidType)size;
        transform.localScale = Vector3.one * size;
        rigidBody.mass = size;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((int)type > 1)
        {
            GameObject newAsteroid = GameController.instance.generateAsteroid();
            newAsteroid.GetComponent<AsteroidController>().updateAsteroid((int)type - 1, transform.position, direction);
            GameObject newAsteroid2 = GameController.instance.generateAsteroid();
            newAsteroid2.GetComponent<AsteroidController>().updateAsteroid((int)type - 1, transform.position, direction);
        }

        Destroy(gameObject);
    }
}

using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public int speed;
    public AsteroidType type;

    private PolygonCollider2D polygonCollider;
    private Rigidbody2D rigidBody;
    private Vector2 direction;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        
        Destroy(gameObject, GameController.instance.lifetime);
    }

    public void createRandomAsteroid()
    {
        assignSize(Random.Range(1, 4));

        float screenHeight = Camera.main.orthographicSize * 2, screenWidth = screenHeight * Camera.main.aspect;
        Vector2 randomPosition = Random.insideUnitCircle.normalized;
        transform.position = new Vector3(randomPosition.x * screenWidth, randomPosition.y * screenHeight, 1);

        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        
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

    public void updateAsteroid(int type, Vector2 direction)
    {
        assignSize(type);
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
        if (collision.gameObject.tag != "Asteroid")
        {
            if ((int)type > 1)
            {
                splitAsteroid(collision.transform.up);
                splitAsteroid(collision.transform.up);
            }

            GameController.instance.destroyAsteroid(transform.position);
            Destroy(gameObject);
        }
    }

    private void splitAsteroid(Vector2 rotation)
    {
        GameObject asteroid = Instantiate(gameObject, transform.position, transform.rotation);
        asteroid.GetComponent<AsteroidController>().updateAsteroid((int)(type - 1),
            new Vector2(Random.Range(rotation.x - 10, rotation.x + 10), Random.Range(rotation.y - 10, rotation.y + 10)).normalized);
    }
}

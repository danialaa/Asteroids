using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public CircleCollider2D circleCollider;
    public int speed;

    private Rigidbody2D rigidBody;
    private AsteroidType type;
    private Vector2 direction;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        createRandomAsteroid();
    }

    private void createRandomAsteroid()
    {
        assignSize(Random.Range(1, 4));

        double directionX = Random.Range(-1, 1);
        double directionY = Random.Range(-1, 1);
        direction = new Vector2((float)directionX, (float)directionY).normalized;

        rigidBody.AddForce(direction * speed);
    }

    public void updateAsteroid(int type, Vector2 direction)
    {
        assignSize(type);
        rigidBody.AddForce(direction * speed);
    }

    private void assignSize(int size)
    {
        type = (AsteroidType)size;
        transform.localScale = Vector3.one * size;
        rigidBody.mass = size;
    }

    public void destroyAsteroid()
    {
        if((int)type > 1)
        {
            GameObject newAsteroid = GameController.instance.generateAsteroid();
            newAsteroid.GetComponent<AsteroidController>().updateAsteroid((int)type - 1, direction);
            GameObject newAsteroid2 = GameController.instance.generateAsteroid();
            newAsteroid2.GetComponent<AsteroidController>().updateAsteroid((int)type - 1, direction);
        }

        GameController.instance.removeAsteroid(gameObject);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public CapsuleCollider2D capsuleCollider;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, GameController.instance.lifetime);
        Physics2D.IgnoreCollision(GameController.instance.player.capsuleCollider, capsuleCollider);

        rigidBody.AddForce(transform.up * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            GameController.instance.increaseScore((int)collision.gameObject.GetComponent<AsteroidController>().type * 10);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.collider, capsuleCollider);
        }    
    }
}

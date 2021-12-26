using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public CapsuleCollider2D capsuleCollider;

    private Rigidbody2D rigidBody;
    private bool canHitShip = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, GameController.instance.lifetime);
        Physics2D.IgnoreCollision(GameController.instance.player.capsuleCollider, capsuleCollider);
        Invoke("switchFriendlyHit", 3);

        rigidBody.AddForce(transform.up * speed);
    }

    private void switchFriendlyHit()
    {
        canHitShip = true;
        Physics2D.IgnoreCollision(GameController.instance.player.capsuleCollider, capsuleCollider, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            //add score
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player" && canHitShip)
        {
            //remove life
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.collider, capsuleCollider);
        }    
    }
}

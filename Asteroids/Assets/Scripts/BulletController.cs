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

        rigidBody.AddForce(transform.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            //add score
        }
        else if (collision.gameObject.tag == "Player")
        {
            //remove life
        }    

        Destroy(gameObject);
    }
}

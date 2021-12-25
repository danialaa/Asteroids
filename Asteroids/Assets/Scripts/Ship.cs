using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;
    public Rigidbody2D rigidBody;
    public CapsuleCollider2D capsuleCollider;
    
    public void directShip(int direction)
    {
        rigidBody.AddTorque(rotationSpeed * direction);
    }

    public void moveShip()
    {
        Vector2 addedVelocity = transform.up * acceleration * Time.deltaTime;

        rigidBody.velocity += addedVelocity;

        rigidBody.velocity = new Vector2(Mathf.Min(maxSpeed, rigidBody.velocity.x), Mathf.Min(maxSpeed, rigidBody.velocity.y));
        rigidBody.velocity = new Vector2(Mathf.Max(-maxSpeed, rigidBody.velocity.x), Mathf.Max(-maxSpeed, rigidBody.velocity.y));
    }
}
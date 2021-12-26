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

    private bool isFixingX = false, isFixingY = false;

    private void Update()
    {
        warp();
    }

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

    private void warp()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1 || Camera.main.WorldToViewportPoint(transform.position).x < 0)
        {
            if (!isFixingX)
            {
                transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
                isFixingX = true;
            }
        }
        else
        {
            isFixingX = false;
        }
        if (Camera.main.WorldToViewportPoint(transform.position).y > 1 || Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            if (!isFixingY)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y * -1, transform.position.z);
                isFixingY = true;
            }
        }
        else
        {
            isFixingY = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //remove life
    }
}
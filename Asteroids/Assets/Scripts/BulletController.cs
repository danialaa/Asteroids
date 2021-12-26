using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public CapsuleCollider2D capsuleCollider;

    public void shootBullet()
    {
        Vector2 newPosition = transform.up * speed * Time.deltaTime;

        transform.position += new Vector3(newPosition.x, newPosition.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gameObject.SetActive(false);
    }
}

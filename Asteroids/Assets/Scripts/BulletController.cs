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

        transform.position += new Vector3(newPosition.x, newPosition.y, transform.position.z);

        if (capsuleCollider.isTrigger)
        {
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Ship player;

    void Start()
    {
        
    }

    void Update()
    {
        moveShip();
        directShip();
        shoot();
    }

    private void directShip()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.directShip(1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.directShip(-1);
        }
    }

    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void moveShip()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.moveShip();
        }
    }
}

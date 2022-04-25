using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandMovement : MonoBehaviour
{
    public BossShockwave bossShockwave;
    public bool leftHand;
    
    private float speed = .05f;
    private bool changeDirection;

    private void Start()
    {
        if (leftHand)
            changeDirection = true;
    }

    void Update()
    {
        if (changeDirection)
        {
            speed *= -1f;
            changeDirection = false;
        }
        
        Vector3 moveDirection = new Vector3(0f, 1f, 0f);

        transform.position += moveDirection * speed;

        if (transform.position.y < 0f || transform.position.y > 10f)
        {
            changeDirection = true;
            
            if (transform.position.y < 2f)
            {
                bossShockwave.StartShockwave();
            }
        }
    }
}

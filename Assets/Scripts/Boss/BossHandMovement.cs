using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandMovement : MonoBehaviour
{
    public BossShockwave bossShockwave;
    public bool leftHand;
    
    [SerializeField]
    private float speed = 5f;
    private bool _changeDirection;

    private void Start()
    {
        if (leftHand)
            _changeDirection = true;
    }

    void Update()
    {
        if (_changeDirection)
        {
            speed *= -1f;
            _changeDirection = false;
        }
        
        Vector3 moveDirection = new Vector3(0f, 1f, 0f);

        transform.position += moveDirection * (speed * Time.deltaTime);

        if (transform.position.y < 0f || transform.position.y > 10f)
        {
            _changeDirection = true;
            
            if (transform.position.y < 2f)
            {
                bossShockwave.StartShockwave();
            }
        }
    }
}

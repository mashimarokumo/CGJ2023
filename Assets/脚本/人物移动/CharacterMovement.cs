using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        Movement();
    }


    void Movement ()
    {
        if (Input.GetAxis("Horizontal")>0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Input.GetAxis("Horizontal") <0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

    }
}   

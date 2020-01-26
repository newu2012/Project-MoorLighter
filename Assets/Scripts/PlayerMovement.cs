using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    public Rigidbody2D rb;
    public Animator animator;
    
    private Vector2 movement;
    private Vector2 lastDir;
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.magnitude > 0)
        {
            lastDir = movement;
        }
        
        movement.Normalize();
        
        animator.SetFloat("LastHorizontal", lastDir.x);
        animator.SetFloat("LastVertical", lastDir.y);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

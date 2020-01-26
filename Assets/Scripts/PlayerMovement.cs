using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float moveSpeed = 1.5f;

    public Rigidbody2D rb;
    public Animator animator;
    
    private Vector2 movement;
    private Vector2 lastDir;
    
    
    private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
    private static readonly int LastVertical = Animator.StringToHash("LastVertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (movement.magnitude > 0) //Если движение ещё есть, то обновляем последнее направление для стояния
            lastDir = movement;
        movement.Normalize();
        
        animator.SetFloat(LastHorizontal, lastDir.x);
        animator.SetFloat(LastVertical, lastDir.y);
        animator.SetFloat(Horizontal, movement.x);
        animator.SetFloat(Vertical, movement.y);
        animator.SetFloat(Speed, movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }
}

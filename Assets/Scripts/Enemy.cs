using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HealthSystem
{
    
    #region dataFields

    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    private Vector2 lastDir;
    private float dazedTime;
    public float startDazedTime;
    #endregion
    #region StringToHash
    private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
    private static readonly int LastVertical = Animator.StringToHash("LastVertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    #endregion

    // Update is called once per frame
    void Update()
    {
        movement.x = 1;
        if (movement.magnitude > 0)
            lastDir = movement;
        movement.Normalize();
        animator.SetFloat(LastHorizontal, lastDir.x);
        animator.SetFloat(LastVertical, lastDir.y);
        animator.SetFloat(Horizontal, movement.x);
        animator.SetFloat(Vertical, movement.y);
        animator.SetFloat(Speed, movement.sqrMagnitude);
        if (dazedTime <= 0)
            moveSpeed = 1;
        else
        {
            moveSpeed = 0;
            dazedTime -= Time.deltaTime;
        }
        if (health <= 0)
            Destroy(gameObject);
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }

    public new void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        health -= damage;
        Debug.Log(damage + " damage taken");
    }
}

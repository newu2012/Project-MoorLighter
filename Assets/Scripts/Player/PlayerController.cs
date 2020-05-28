using System;
using DragonBones;
using UnityEngine;
using Transform = UnityEngine.Transform;

public class PlayerController : HealthSystem
{
    #region dataFields

    private float moveSpeed = 2f;
    public Rigidbody2D rb;
    //public Animator animator;
    private Vector2 movement;
    private float attackRate = 0.5f;
    private Vector2 lastDir;
    public Transform attackPos;
    //public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    public int armor;
    private Camera _camera;

    private bool walkSwtich = true;
    #endregion

    #region StringToHash
    private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
    private static readonly int LastVertical = Animator.StringToHash("LastVertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    #endregion
    private void Start()
    {
        healthBar = GameObject.FindWithTag("PlayerHealthSphere").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        _camera = Camera.main;
    }
    //Basic Input
    #region InputInUpdate

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (movement.magnitude > 0)
        {
            lastDir = movement;
            if (walkSwtich)
            {
                GetComponent<UnityArmatureComponent>().animation.Play("walk", 0);
                walkSwtich = false;
            }
        }
        movement.Normalize();

        if (Input.GetMouseButtonDown(0))
            InvokeRepeating( nameof(Attack),0.1f, attackRate);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 4f;
            walkSwtich = false;
            GetComponent<UnityArmatureComponent>().animation.Play("run(HandBehindTheBack)", 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 2f;
            GetComponent<UnityArmatureComponent>().animation.Play("walk", 0);
        }
        
        if (Input.GetMouseButtonUp(0))
            CancelInvoke(nameof(Attack));
        
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(5);

        //if (movement.x < 0)
        //    this.GetComponent<Armature>().flipX;
        
        //animator.SetFloat(LastHorizontal, lastDir.x);
        //animator.SetFloat(LastVertical, lastDir.y);
        //animator.SetFloat(Horizontal, movement.x);
        //animator.SetFloat(Vertical, movement.y);
        //animator.SetFloat(Speed, movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }
    #endregion
    
    //Attack starts here
    #region AttackImplementation

    private void Attack()
    {
        var subjectsToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
        foreach (var subject in subjectsToDamage)
        {
            if (subject.CompareTag("Enemy"))
                subject.GetComponent<Enemy>().TakeDamage(damage);
            else if (subject.CompareTag("Tree"))
                subject.GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    #endregion


    #region ChangeFastAccess
    public void ChangeDamage(int damage, bool join)
    {
        if (join)
            this.damage += damage;
        else
            this.damage -= damage;
    }

    public void ChangeArmor(int armor, bool join)
    {
        if (join)
            this.armor += armor;
        else
            this.armor -= armor;
    }

    #endregion
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float moveSpeed = 1.5f;

    public Rigidbody2D rb;
    public Animator animator;
    
    private Vector2 movement;
    private float fireRate = 0.5f;
    private Vector2 lastDir;
    
    
    private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
    private static readonly int LastVertical = Animator.StringToHash("LastVertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (movement.magnitude > 0) //Если движение ещё есть, то обновляем последнее направление для стояния
            lastDir = movement;
        movement.Normalize();
        
        if (Input.GetMouseButtonDown(0))
            InvokeRepeating( "ShootTo",0.00001f, fireRate);
        
        if (Input.GetMouseButtonUp(0))
            CancelInvoke("ShootTo");
        
        animator.SetFloat(LastHorizontal, lastDir.x);
        animator.SetFloat(LastVertical, lastDir.y);
        animator.SetFloat(Horizontal, movement.x);
        animator.SetFloat(Vertical, movement.y);
        animator.SetFloat(Speed, movement.sqrMagnitude);
    }

    
    //Shooting starts here
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }
    #region Properties

    public GameObject projectilePrefab;
    private Camera _camera;

    #endregion

    #region Implementation

    private Projectile InstantiateNewProjectile()
    {
        var newProjectile = Instantiate(projectilePrefab);
        newProjectile.transform.position = transform.position;
        return newProjectile.GetComponent<Projectile>();
    }

    public void ShootTo()
    {
        Vector2 worldMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        var pDirection = (worldMousePosition - (Vector2)transform.position).normalized;
        var newProjectile = InstantiateNewProjectile();
        newProjectile.ShootTo(pDirection);
    }

    #endregion
}
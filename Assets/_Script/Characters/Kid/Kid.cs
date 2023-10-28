using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Singleton<Kid> , IDamageable , IMoveable
{

    public delegate void HealthChangeHandle(float currentHealth, float maxHealth);
    public static HealthChangeHandle OnUpdatePlayerHealth;
    public Transform Transform { get; private set; }
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public bool IsFacingRight { get; set; }

    public float HealthPoint = 5f;
    public float MoveSpeed = 0.3f;

    [SerializeField] private Animator _animator;
    public void Die()
    {
        Debug.Log("GAME OVER");
    }

    public void Move(Vector2 velocity)
    {
        if (velocity == Vector2.zero)
        {
            _animator.Play("idle");
        }
        else
        {
            _animator.Play("walk");
        }
        Rigidbody.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    private void MovePlayer()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(inputX, inputY).normalized;
        Move(moveDirection * MoveSpeed);
    }

    public void TakeDamage(float damage)
    {
        // Kid only take maximum 1 damage each.
        CurrentHealth -= 1f;
        UpdateHealthBar();
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void UpdateHealthBar()
    {
        OnUpdatePlayerHealth?.Invoke(CurrentHealth, MaxHealth);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        Rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Transform = transform;
        CurrentHealth = MaxHealth = HealthPoint;
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    private void Update()
    {
        MovePlayer();
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }
}

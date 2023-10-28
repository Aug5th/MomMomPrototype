using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Singleton<Kid> , IDamageable
{

    public delegate void HealthChangeHandle(float currentHealth, float maxHealth);
    public static HealthChangeHandle OnUpdatePlayerHealth;
    public Transform Transform { get; private set; }
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public bool IsFacingRight { get; set; }

    public float HealthPoint = 5f;
    public float MoveSpeed = 1f;

    [SerializeField] private Animator _animator;
    public void Die()
    {
        Debug.Log("GAME OVER");
    }

    public void Move(Vector3 destination)
    {
        Vector2 direction = (destination - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, destination, MoveSpeed * Time.deltaTime);
        _animator.Play("walk");
        CheckForLeftOrRightFacing(direction);
    }

    public void StopMoving()
    {
        _animator.Play("idle");
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
        _animator = GetComponent<Animator>();
        Transform = transform;
        CurrentHealth = MaxHealth = HealthPoint;
    }

    private void Start()
    {
        UpdateHealthBar();
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

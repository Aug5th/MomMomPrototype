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
    public Transform Target { get; set; }
    public bool IsHavingTarget { get; set; }

    [Header("Kid Stats")]
    [SerializeField] private float _healthPoint;
    [SerializeField] private float _manaPoint;
    [SerializeField] private float _moveSpeed;

    public void Die()
    {
        Debug.Log("GAME OVER");
    }

    public void Move(Vector2 velocity)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        // Kid only take maximum 1 damage each.
        CurrentHealth -= 1f;
        OnUpdatePlayerHealth?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void UpdateHealthBar()
    {
        throw new System.NotImplementedException();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        Transform = transform;
        CurrentHealth = MaxHealth = _healthPoint;
        OnUpdatePlayerHealth?.Invoke(CurrentHealth, MaxHealth);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        throw new System.NotImplementedException();
    }
}

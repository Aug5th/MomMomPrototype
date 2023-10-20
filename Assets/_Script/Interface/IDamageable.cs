using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
    void Die();
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    void UpdateHealthBar();
}


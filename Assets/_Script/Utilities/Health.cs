using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MyMonoBehaviour
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public void SetHealth(float maxHealth)
    {
        _currentHealth = maxHealth;
        _maxHealth = maxHealth;
    }

    public void UpdateHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
    }
}

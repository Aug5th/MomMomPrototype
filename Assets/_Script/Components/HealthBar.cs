using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MyMonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private Health _health;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _slider = GetComponentInChildren<Slider>();
        _health = GetComponentInParent<Health>();
    }

    private void FixedUpdate()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _slider.value = _health.CurrentHealth / _health.MaxHealth;
    }
}
